#include <QApplication>
#include <QGraphicsScene>
//#include <QGraphicsRectItem> --Replacing with a custom rectangle object
#include "PlayerObject.h"
#include <QGraphicsView>
#include <QKeyEvent>
#include <QTimer>
#include <QObject>

/*
 - QGraphicsScene
 - QGraphicsItem - Abstract class which a new class is supposed to be derived from
 - QGraphicsView - Widget used to visualize the scene. Prints the items on the screen.
 - QKeyEvent - Stores keyPressEvents from the user
 - QDebug - a header file used to print logs to the console
 */

int main(int argc, char *argv[])
{
    QApplication a(argc, argv);

    //Creating variables to store the scene size
    const int sceneWidth = 1000, sceneHeight= 1000;

    //Creating a scene
    QGraphicsScene * mainScene = new QGraphicsScene();

    //Setting the window properties
    mainScene->setSceneRect(0, 0, sceneWidth, sceneHeight);

    //Create an item to put into the scene
    PlayerObject * player = new PlayerObject();
    player->setRect(sceneWidth/2,sceneHeight/2,100,100); //X,Y, Width, Height


    //Add item to the scene
    mainScene->addItem(player);

    //Making an item focusable
    player->setFlag(QGraphicsItem::ItemIsFocusable);
    player->setFocus();

    //Creating a view to visualize the scene
    QGraphicsView * view = new QGraphicsView(mainScene);
    view->setHorizontalScrollBarPolicy(Qt::ScrollBarAlwaysOff);
    view->setVerticalScrollBarPolicy(Qt::ScrollBarAlwaysOff);
    //view->setFixedSize(sceneWidth,sceneHeight);

    //Making the view widget visible
    view->show();

    //Spawning enemy
    QTimer * timer = new QTimer();
    QObject::connect(timer, SIGNAL(timeout()), player, SLOT(spawn()));

    timer->start(2000);

    return a.exec();
}

