#include "bullet.h"
#include <QTimer>
#include <QGraphicsScene>
#include <QGraphicsView>
#include <QDebug>

Bullet::Bullet(int x, int y)
{

   //Drawing the bullet
   setRect(x,y,3,5);

   //Connect
   QTimer * timer = new QTimer();
   connect(timer, SIGNAL(timeout()), this, SLOT(move()));

   //Bullet moves at 50 milliseconds
   timer->start(50);

}

void Bullet::move()
{
    //Move the bulllet up
    setPos(x(), y()-10);


    //Deleting the bullet if it escpates the screen
    if(this->pos().y() < 0-(scene()->views().first()->height()/2) )
    {
        //Removing from the scene
        scene()->removeItem(this);

        //Deleting the bullet
        delete this;

        qDebug() << "Bullet deleted";
    }

}
