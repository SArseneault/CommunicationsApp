#include "enemy.h"
#include <QTimer>
#include <QGraphicsScene>
#include <QGraphicsView>
#include <QDebug>
#include <stdlib.h>


enemy::enemy(): QObject(), QGraphicsRectItem()
{
   //Ceating random location
    int randomNumber = rand() % 500;//(scene()->views().first()->height()-100);
    setPos(randomNumber,0);

   //Drawing the bullet
   setRect(0,0,75,75);

   //Connect
   QTimer * timer = new QTimer();
   connect(timer, SIGNAL(timeout()), this, SLOT(move()));

   //Bullet moves at 50 milliseconds
   timer->start(50);

}

void enemy::move()
{
    //Move the enemy down
    setPos(x(), y()+5);

    qDebug() << this->pos().y();
    //Deleting the bullet if it escpates the screen
    if(this->pos().y() > (scene()->views().first()->height()) )
    {
        //Removing from the scene
        scene()->removeItem(this);

        //Deleting the bullet
        delete this;

        qDebug() << "Enemy deleted";
    }

}
