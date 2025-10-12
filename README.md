# XMLTask

## Запуск решения

1. Скачать репозиторий.
2. Открыть решение в VS.
3. Добавить в startup настройки два проекта DataProcessor, XMLParser.
4. Запустить проекты.

Файлы xml будут браться из папки, указанной в appsettings.json в XMLTask.XMLParser в секции :'"ParserConfiguration": "XMLsFolder": "{folderName}"'

Connection string для бд устанавливается в appsettings.json XMLTask.DataProcessor в секции : '"ConnectionStrings": "SqliteXMLTaskConnectionString": "{connectoinString}"'

Host и QueueName для RabbitMQ устанавливаются в двух проектах XMLTask.XMLParser, XMLTask.DataProcessor в appsettings.json в секции : "RabbitMQConfiguration": "Host": "{host}, "QueueName": "{queueName}"