לבודק של תרגיל 3: הגרסה המעודכנת של השרת שעובדת עם אפליקציית האנדררואיד, נמצאת בענף ששמו 
firebaseNotification 
כמו כן, העלתי גרסה של השרת לשרתי האוניברסיטה כפי שהודגם במדריך שהעלתם, שם המשתמש שלי הוא 
schwarye
בהצלחה.

## NOTE: This backend repo contains frontend parts (compiled react project) which are NOT relevant to the backend. Please ignore the wwwroot folder and the app-frontend it contains. The frontend part of the project is in repo "ex1" which is also submitted in the submition text file.
# ChatsServer

How to set server ip:port to '1.2.3.4:1234':


1. open 'Properties/launchSettings.json' and change the values under "applicationUrl" to 'http://1.2.3.4:1234,http://1.2.3.4:1235'.
2. open 'appsettings.json' and change the values under "myLocalIpv4" to '1.2.3.4', change the values under "myPort" to '1234'
and change "ValidAudience" to "http://1.2.3.4:4200" and "ValidIssuer" "http://1.2.3.4:5000" 
3. You might need to execute to following commands in the Package Console Manager:
Add-Migration <something>, Update-Database.
  
  
Dependencies:

- .nuget\packages\bcrypt.net-next\4.0.3\
- .nuget\packages\microsoft.aspnetcore.authentication.jwtbearer\6.0.5\
- .nuget\packages\microsoft.aspnetcore.identity.entityframeworkcore\6.0.5\
- .nuget\packages\microsoft.aspnetcore.spaservices\3.1.25\
- .nuget\packages\microsoft.aspnetcore.spaservices.extensions\6.0.5\
- .nuget\packages\microsoft.entityframeworkcore.sqlite\6.0.5\
- .nuget\packages\microsoft.entityframeworkcore.sqlserver\6.0.5\
- .nuget\packages\microsoft.entityframeworkcore.tools\6.0.5\
- .nuget\packages\microsoft.visualstudio.web.codegeneration.design\6.0.4\
- .nuget\packages\pomelo.entityframeworkcore.mysql\6.0.1\
- .nuget\packages\swashbuckle.aspnetcore\6.3.1\
- .nuget\packages\firebaseadmin\2.3.0\
