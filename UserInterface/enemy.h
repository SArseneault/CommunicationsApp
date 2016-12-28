#ifndef ENEMY_H
#define ENEMY_H

#include <QGraphicsRectItem>
#include "widget.h"
//This class handles signals and slots by inheritings the QObject and creating a QObject macro
class enemy: public QObject, public QGraphicsRectItem
{
    //Creating macro
    Q_OBJECT

    //Public Constructors
public:
    enemy();

    //Public Slot Member functions
public slots:
    void move();
};

#endif // ENEMY_H
