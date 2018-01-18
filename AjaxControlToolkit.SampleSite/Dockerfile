FROM microsoft/iis

RUN powershell Install-WindowsFeature NET-Framework-45-ASPNET ; \  
    Install-WindowsFeature Web-Asp-Net45

RUN mkdir C:\site

RUN powershell -NoProfile -Command \
    Import-module IISAdministration; \
    New-IISSite -Name "Site" -PhysicalPath C:\site -BindingInformation "*:8000:"

EXPOSE 8000

COPY . /site

RUN %windir%\system32\inetsrv\appcmd.exe unlock config -section:system.webServer/handlers