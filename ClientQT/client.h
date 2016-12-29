#ifndef CLIENT_H
#define CLIENT_H

#include <QObject>
#include <QTcpSocket>
#include <QDataStream>
#include <QDebug>
#include <QList>
#include <QString>

class client : public QObject
{
    Q_OBJECT
public:
    // Default Constructor(s)
    explicit client(QObject *parent = 0);
    client(int port);

    // Public Methods
    bool establishConnection();
    bool SendMessage(QByteArray message="");
    QByteArray GetLatestServerMessage();
    QString GetErrorMessages();

private:
    // Private Fields
    int port;
    QTcpSocket *socket;
    QByteArray latestServerMessage;
    QList<QString> errorMessages;


    // Private Helper Methods
    void disconnectServer();

signals:

public slots:


};

#endif // CLIENT_H
