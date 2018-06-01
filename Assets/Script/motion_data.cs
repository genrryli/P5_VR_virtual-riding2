using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO.Ports;
using System.Threading;
using System;

public class motion_data : MonoBehaviour {

    //串口相关参数
    public bool open_io;//串口开关许可
    public string portname = "COM2";
    public int portspeed = 115200;
    private SerialPort ArduinoPort;

    //线程相关参数
    private Thread tport;
    private bool on_thread=false;

    //获取数据
    public float _real_speed = 0;
    public float _real_angle = 0;
    private float _heart_beat = 0;
    private string _big_button;

    void Start () {
        //portname = PlayerPrefs.GetString("com");
        //open_io = Convert.ToBoolean(PlayerPrefs.GetInt("io"));
        ArduinoPort = new SerialPort();
        ArduinoPort.PortName = portname;//串口。使用电脑的22号串口
        ArduinoPort.BaudRate = portspeed;//串口。波特率是9600
        if (open_io) { ArduinoPort.Open(); } else { ArduinoPort.Close(); }//打开串口

        on_thread = true;
        tport = new Thread(new ThreadStart( writedata));//定义线程
        tport.Start();//打开新线程
    }
	
	void FixedUpdate () {
        if (open_io && !tport.IsAlive)//如果串口打开了，即打开线程
        {
            tport = new Thread(new ThreadStart(writedata));//定义线程
            tport.Start();//打开新线程
        }
        if (Time.frameCount % 120 == 0) { System.GC.Collect(); }//清理缓存      
        if (!open_io && tport.IsAlive) { close_port(); }//如果串口关闭，则关闭串口及线程
    }

    void writedata()//新的线程的函数、专门读取数据
    {
        while (on_thread)//当线程打开时执行循环
        {
            string read;         
            read = ArduinoPort.ReadLine();//逐行读取ardunio上的字符串
            ArduinoPort.DiscardInBuffer();//清理缓存区数据
            _big_button = get_button(read);//刷新按钮的数据
            _real_speed = get_speed(read);//刷新速度的数据
            _real_angle = get_angle(read);//刷新角度的数据
            _heart_beat = get_heart_beat(read);//刷新心率的数据
            Debug.Log("" + read);
        }  
    }

    void OnApplicationQuit()//退出程序时所执行的函数
    {
		close_port();
    }

	public void close_port()
	{
		on_thread = false;//跳出死循环
		if (tport.IsAlive) { tport.Abort(); }//关闭线程
		ArduinoPort.Close();//关闭串口
		Debug.Log("---thread killed---port closed---");
	}


    float get_angle(string read)//将字符串转换为角度数据
    {
        float a;
        float data = float.Parse(devide_data(read, 0));
        if (data > 529) { a = (data - 529) / (529 - 1) * 135; }
        else if (data < 529 - 1) { a = (data - 529) / (529 - 1) * 135; }
        else { a = 0; }
        return -(int)a;
    }

    string get_button(string read)//将字符串转换为按钮数据
    {
        string button_event;
        button_event = devide_data(read,1);
        return button_event ;
    }

    float get_speed(string read)//将字符串转换为速度数据
    {
        float speed= float.Parse(devide_data(read, 2)); 
        return speed;
    }

    float get_heart_beat(string read)//将字符串转换为心率数据
    {
        float heat_beat = float.Parse(devide_data(read, 3));
        return heat_beat;
    }

    string devide_data(string data,int num)//以“；”分割数据包
    {
        string[] Data = data.Split(';');
        return Data[num];
    }

    public string big_button
    {
        get { return _big_button; }
    }

    public float real_speed
    {
        get { return _real_speed; }
    }

    public float real_angle
    {
        get { return _real_angle; }
    }

    public float heart_beat
    {
        get { return _heart_beat; }
    }
}
