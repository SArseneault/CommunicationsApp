//Implementation file for the rectangleobject header

#include "PlayerObject.h"
#include <QDebug>
#include <QKeyEvent>
#include <bullet.h>
#include <QGraphicsScene>
#include <QGraphicsView>
#include <enemy.h>

void PlayerObject::keyPressEvent(QKeyEvent *event)
{
    //Checking which key has been pressed
    if (event->key() == Qt::Key_Left)
    {
        if(pos().x() > 0-(scene()->views().first()->width()/2))
        {
            setPos(x()-10, y());
        }
        qDebug() << "Input: Left Key";
    }else if (event->key() == Qt::Key_Right)
    {
        if(pos().x()+this->rect().width() < scene()->views().first()->width()/2)
        {
            setPos(x()+10, y());
        }
        qDebug() << "Input: Right Key";
    }else if (event->key() == Qt::Key_Up)
    {
        setPos(x(), y()-10);
        qDebug() << "Input: Up Key";
    }else if (event->key() == Qt::Key_Down)
    {
        setPos(x(), y()+10);
        qDebug() << "Input: Down Key";
    }else if(event->key() == Qt::Key_Space)
    {
        //Create a bullet
        Bullet * bullet = new Bullet(this->rect().x(),this->rect().y());
        bullet->setPos(x(),y());

        //Add the bullet to the scene
        scene()->addItem(bullet);
        qDebug() << "Input: Space";
    }


}

void PlayerObject::spawn()
{
    //Create an enemy
    enemy * Enemy = new enemy();
    scene()->addItem(Enemy);


}
