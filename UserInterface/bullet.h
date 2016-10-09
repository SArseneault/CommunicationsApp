#ifndef BULLET_H
#define BULLET_H

#include <QGraphicsRectItem>
#include "widget.h"
//This class handles signals and slots by inheritings the QObject and creating a QObject macro
class Bullet: public QObject, public QGraphicsRectItem
{
    //Creating macro
    Q_OBJECT

    //Public Constructors
public:
    Bullet(int x, int y);

    //Public Slot Member functions
public slots:
    void move();
};

#endif // BULLET_H
