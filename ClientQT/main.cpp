#include <QCoreApplication>
#include <QDebug>
#include <QString>
#include "client.h"


int main(int argc, char *argv[])
{
    QCoreApplication a(argc, argv);

    //Opening a text stream
    QTextStream cin(stdin);

    //Creating a new client - *This port number should be stored in config file.
    client ClientObj(1302);

    //Establishing a connection
    ClientObj.establishConnection();
    qDebug() << "Establishing connection...";
    ClientObj.establishConnection();

    QByteArray message = "";
    QByteArray response = "";

    while(true)
    {
        //Retreieve message from user
        qDebug() << "Send message to server: ";
        message = cin.readLine().toLatin1().data();
        qDebug() << message;

        //Sending message to server
        if(!ClientObj.SendMessage(message))
        {
            qDebug() << "ERROR: " << ClientObj.GetErrorMessages();
        }
        else
        {
            //Retrieve the servers response
            qDebug() << "Server response: " << ClientObj.GetLatestServerMessage();
        }

    }


    return a.exec();
}
