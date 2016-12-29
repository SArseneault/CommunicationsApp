#include "client.h"
#include <QByteArray>
#include <QDateTime>
#include <QString>

/*
 * Public Methods
 */
bool client::establishConnection()
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


    return true;

}

bool client::SendMessage(QByteArray message)
{

    QByteArray response = "";
    bool reconnectFlag = false;

     //Sending message to server
     if(socket->write(message) == -1)
     {
         reconnectFlag = true;
         errorMessages.append("There was an error writing data to the server.");
     }

     //Retrieve message from server
     if(!socket->waitForBytesWritten(1000))
     {
         reconnectFlag = true;
         errorMessages.append("There was an error waiting for bytes to be written.");
     }

     if(!socket->waitForReadyRead(3000))
     {
         reconnectFlag = true;
         errorMessages.append("There was an error waiting for ready read.");
     }

     response = socket->readAll();



   if(reconnectFlag)
   {
       //Re-establish the connection
       disconnectServer();
       establishConnection();


       return false;
   }


    //Store the repsonse
    latestServerMessage = response;

    return true;

}

/*
 * Private Helper Methods
 */
void client::disconnectServer()
{
    socket->close();
}

/*
 * Getters and Setters
 */
QByteArray client::GetLatestServerMessage()
{
    return latestServerMessage;
}

QString client::GetErrorMessages()
{
    //Creating a string which will append all of the error strings
    QString errorsAppended = "";


    foreach(QString error, errorMessages)
    {
        errorsAppended += error + " ";
    }

    //Clearing the error messages since they have been read
    errorMessages.clear();


    return errorsAppended;
}

/*
 * Default Constructors
 */
client::client(int port)
{
    //Storing the port
    this->port = port;

}

client::client(QObject *parent) : QObject(parent)
{

}



