c: cd
cd c:\Windows\System32\inetsrv\
appcmd add site /name:InfoPortal /id:3 /physicalPath:%1 /bindings:http/*:8080: