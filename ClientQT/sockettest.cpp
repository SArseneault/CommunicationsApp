#include "sockettest.h"

SocketTest::SocketTest(QObject *parent) : QObject(parent)
{

}

void SocketTest::Connect()
{
    socket = new QTcpSocket(this);

    //Connect to the server
    socket->connectToHost("127.0.0.1", 1302);

    if(socket->waitForConnected(3000))
    {
        qDebug() << "Connected!";

        //Send Data
        socket->write("Hello Server\r\n\r\n");

        //Retrieve Response
        socket->waitForBytesWritten(1000);
        socket->waitForReadyRead(3000);
        qDebug() << "Reading: " << socket->bytesAvailable();
        qDebug() << socket->readAll();

        socket->close();
    }
    else
    {
        qDebug() << "Not Connected";
    }




}
