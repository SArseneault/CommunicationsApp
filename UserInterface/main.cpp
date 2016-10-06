#include <QApplication>
#include <QGraphicsScene>
#include <QGraphicsRectItem>
#include <QGraphicsView>

/*
 - QGraphicsScene
 - QGraphicsItem - Abstract class which a new class is supposed to be derived from
 - QGraphicsView - Widget used to visualize the scene. Prints the items on the screen.
 */

int main(int argc, char *argv[])
{
    QApplication a(argc, argv);

    //Creating a scene
    QGraphicsScene * mainScene = new QGraphicsScene();

    //Create an item to put into the scene
    QGraphicsRectItem * rect = new QGraphicsRectItem();
    rect->setRect(0,0,100,100); //X,Y, Width, Height

    //Add item to the scene
    mainScene->addItem(rect);

    //Add a view
    QGraphicsView * view = new QGraphicsView(mainScene);

    //Making the view widget visible
    view->show();

    return a.exec();
}

