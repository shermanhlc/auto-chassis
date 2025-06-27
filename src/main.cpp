#include <QDebug>

#include <QString>
#include <QCoreApplication>
#include <QCommandLineParser>

#define TOML_EXCEPTIONS 0  // must be defined before
#include <toml++/toml.hpp>

#include <utils/point.h>
#include <utils/equations.h>


const QString QtMsgTypeToString(QtMsgType type)
{
    switch (type)
    {
        case QtInfoMsg: return "INFO";
        case QtDebugMsg: return "DEBUG";
        case QtFatalMsg: return "FATAL";
        case QtWarningMsg: return "WARN";
        case QtCriticalMsg: return "CRIT";
        default: return "UNKNOWN";
    }
}


void setupLoggingHandler(const QtMsgType type, const QMessageLogContext& context, const QString& msg)
{
    QByteArray msgData = msg.toLocal8Bit().constData();
    QString log;

    switch(type) {
        // these support color codes too
        case QtDebugMsg:
            log = QString("[%1] %2:%3 \033[31m%4\033[0m - %5")
                    .arg("DEBUG")
                    .arg(context.file)
                    .arg(context.line)
                    .arg(context.function)
                    .arg(msgData);
            break;
        default:
            log = QString("[%1] %2")
                    .arg(QtMsgTypeToString(type))
                    .arg(msgData);
            break;                 
    }

    fprintf(stderr, "%s\n", log.toLocal8Bit().constData());
}


int main(int argc, char* argv[])
{
    QCoreApplication app(argc, argv);
    QCoreApplication::setApplicationName("autochassis");
    QCoreApplication::setApplicationVersion(APP_VERSION);


    // parser for cli options
    QCommandLineParser qparser;
    qparser.setApplicationDescription("AutoChassis is a automated optimal design program for creating a SAE Baja compliant chassis.");
    qparser.addHelpOption();
    qparser.addVersionOption();

    QCommandLineOption configPathOption(
        {"c", "config-path"}, 
        "Set path for the .toml configuration file", 
        "filepath"
    );
    QCommandLineOption iterationStepOption(
        {"i", "iteration-step"}, 
        "Step size for checking each point along a tube", 
        "step"
    );
    qparser.addOptions({
        configPathOption,
        iterationStepOption
    });
    qparser.process(app);
    
    // logging
    qInstallMessageHandler(setupLoggingHandler);

    // toml parse
    if(qparser.isSet(configPathOption))
    {
        QString configPath = qparser.value(configPathOption);
        toml::parse_result result = toml::parse_file(configPath.toStdString());  // this is now effectively a table&

        if(result.failed())
        {
            qWarning().noquote() << "Failed to parse .toml:" << result.error().description();
            return 1;
        }
        
        qInfo()  << "config:" << configPath;
    }

    // do something with the table


    qDebug() << "running with version:" << APP_VERSION;
    qInfo()  << "Run complete";

    // app.exec();
    return 0;
}
