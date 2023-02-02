# Microservices 
The application uses a real-world example with users that can write posts. The user microservice allows creating and editing users. In the user domain, the user entity has several properties like name, mail, etc. In the post domain, there is also a user so the post microservice can load posts and display the writers without accessing the user microservice.The microservices access their example SQLServer databases via Entity Framework and exchange messages via RabbitMQ (e.g. on Docker Desktop).

Used RabbitMQ, C#, REST-API and Entity Framework for asynchronous decoupled communication and eventually consistency with integration events and publish-subscribe.


RabbitMQ set up:
The easiest way to get RabbitMQ running is to install Docker Desktop. Then issue the following command (in one line in a console window) to start a RabbitMQ container with admin UI :
C:\dev>docker run -d  -p 15672:15672 -p 5672:5672 --hostname my-rabbit --name some-rabbit rabbitmq:3-management

Open your browser on port 15672 and log in with the username “guest” and the password “guest”. Use the web UI to create an Exchange with the name “user” of type “Fanout” and two queues “user.postservice” and “user.otherservice”.

It is important to use the type “Fanout” so that the exchange copies the message to all connected queues.

Application developement flow
1) Created the .NET Core Microservices
2) Used RabbitMQ and Configure Exchanges and Pipelines
3) Publish and Consume Integration Events in the Microservices
4) Tested the Workflow
