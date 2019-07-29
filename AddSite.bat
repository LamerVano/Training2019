c: cd
cd c:\Windows\System32\inetsrv\
appcmd add site /name:InfoPortalBack /id:2 /physicalPath:%1 /bindings:http/*:50592: