#include <iostream>
#include <QDebug>

int main(int argc, char* argv[])
{
    std::cout << "Hello!" << std::endl;

    qInfo() << "Hello from Qt!";

    return 0;
}
