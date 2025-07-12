using System.Net;
using System.Net.Sockets;
using OscCore;

namespace XRMiniLink;

public class SimpleOSC
{
    private readonly UdpClient _udpClient;
    private readonly IPEndPoint _remoteEndPoint;
    private readonly Thread _readThread;
    private bool _connected;

    //event for receiving OSC messages
    public event Action<OscMessageRaw>? MessageReceived;
    
    public SimpleOSC(IPEndPoint listenerEndPoint, IPEndPoint remoteEndPoint)
    {
        _udpClient = new UdpClient(listenerEndPoint);
        _remoteEndPoint = remoteEndPoint;
        _readThread = new Thread(ReceiveMessages)
        {
            IsBackground = true
        };
    }
    
    public void StartReceiving()
    {
        _udpClient.Connect(_remoteEndPoint);
        _connected = true;
        _readThread.Start();
        //xremote thread
        _ = Task.Run(async () =>
        {
            while (_connected)
            {
                var message = new OscMessage("/xremote");
                var data = message.ToByteArray();
                try
                {
                    await _udpClient.SendAsync(data, data.Length);
                    //Console.WriteLine("Sent: " + message.Address);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error sending OSC message: {e.Message}");
                    _connected = false; // Stop the thread on error
                }
                await Task.Delay(10000); // 10 seconds
            }
        });
        Console.WriteLine("Connected to " + _remoteEndPoint);
    }

    private void ReceiveMessages()
    {
        while (_connected)
        {
            try
            {
                var ep = _remoteEndPoint;
                var result = _udpClient.Receive(ref ep);
                var message = new OscMessageRaw(result, ep, OscTimeTag.UtcNow);
                MessageReceived?.Invoke(message);
                Console.WriteLine($"Received: {message.Address}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error receiving OSC message: {e.Message}");
                _connected = false; // Stop the thread on error
            }
        }
    }
    
    public void StopReceiving()
    {
        _connected = false;
        _readThread.Join();
        _udpClient.Close();
        Console.WriteLine("Stopped receiving OSC messages.");
    }

    public void SendOSCMessage(OscMessage message)
    {
        var data = message.ToByteArray();
        try
        {
            _udpClient.Send(data, data.Length);
            Console.WriteLine($"Sent OSC message: {message.Address}");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error sending OSC message: {e.Message}");
        }
    }

    public async Task SendOSCMessageAsync(OscMessage message)
    {
        var data = message.ToByteArray();
        try
        {
            await _udpClient.SendAsync(data, data.Length);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error sending OSC message: {e.Message}");
        }
    }
    
    public void TriggerValue(OscMessage message)
    {
        //Send message to get value from remote
        var data = message.ToByteArray();
        try
        {
            _udpClient.Send(data, data.Length);
            //Console.WriteLine($"Sent trigger message: {message.Address}");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error sending trigger message: {e.Message}");
        }
    }
}