#ifndef CLIENT_H
#define CLIENT_H

#include <QObject>
#include <QTcpSocket>
#include <QDataStream>
#include <QDebug>

class client : public QObject
{
    Q_OBJECT
public:
    //Default Constructor(s)
    explicit client(QObject *parent = 0);

    client(int port);

    // Public Methods
    void establishConnection();
    const char* SendMessage(const char * message="");
    void disconnectServer();


private:
    //Private Fields
    int port;
    QTcpSocket *socket;

signals:

public slots:


};

#endif // CLIENT_H
