%systemroot%\system32\inetsrv\appcmd add site /name:InfoPortalBack /physicalPath:d:\Vano1\Git\InfoPortal\Publish\ /bindings:http/*:50592:
%systemroot%\system32\inetsrv\appcmd set app "InfoPortalBack/" /applicationPool:"ASP.NET v4.0"