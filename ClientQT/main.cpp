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

    const char * message = "";
    const char * response = "";

    while(true)
    {
        qDebug() << "Send message to server: ";
        message = cin.readLine().toLatin1().data();
        qDebug() << message;

        response = ClientObj.SendMessage("NOPE");

        qDebug() << "Server response: " << response;
    }


    return a.exec();
}
