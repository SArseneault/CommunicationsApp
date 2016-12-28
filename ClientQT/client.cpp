#include "client.h"

client::client(QObject *parent) : QObject(parent)
{

}


void client::establishConnection()
{
    socket = new QTcpSocket(this);

    //Creating a counter for the connection attempts
    int connectionAttempt = 0;


    //Attempt to connect to the server
    socket->connectToHost("127.0.0.1", this->port);

    //
    while(!socket->waitForConnected(1000))
    {
        connectionAttempt++;

        qDebug() << "Current Connection Attempt #: " << connectionAttempt;
        socket->connectToHost("127.0.0.1", this->port);


    }

    qDebug() << "Connected to server!";

    /*
    if(socket->waitForConnected(3000))
    {
        qDebug() << "Connected!";

        //Send Data
        socket->write("hello server\r\n\r\n");

        //Retrieve Response
        socket->waitForBytesWritten(1000);
        socket->waitForReadyRead(3000);
        qDebug() << "Reading: " << socket->bytesAvailable();
        qDebug() << socket->readAll();

       // socket->close();
    }
    else
    {
        qDebug() << "Not Connected";
    }
    */



}

const char * client::SendMessage(const char * message)
{
    const char * response = "";
    qint64 responseBytes;

    try
    {
        //Sending message to server
        socket->write(message);

        //Retreieve message from server
        socket->waitForBytesWritten(1000);
        socket->waitForReadyRead(3000);
        responseBytes = socket->bytesAvailable();

        response = socket->readAll();


        qDebug() << response;
    }
    catch(int)
    {
        qDebug() << "Lost Connection to Server";
        disconnectServer();
        establishConnection();
    }

    return response;

}

void client::disconnectServer()
{
    socket->close();
}



client::client(int port)
{
    //Storing the port
    this->port = port;
}
