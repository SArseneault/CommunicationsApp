#ifndef MYTHREAD_H
#define MYTHREAD_H

#include <QThread>
#include <QTcpSocket>
#include <QDebug>

/*Every time a new connection is made, a new thread is created*/
class MyThread : public QThread
{
    Q_OBJECT
public:
    explicit MyThread(int ID, QObject *parent = 0);
    void run();

signals:
    void error(QTcpSocket::SocketError socketerror);

public slots:
    //Slots of the socket
    void readyRead();
    void disconnected();

private:
    QTcpSocket * socket;
    int socketDescriptor; //Underlying socket id number from OS


};

#endif // MYTHREAD_H
