@echo off
pushd "%~dp0"

start ttyd.exe --interface 127.0.0.1 --port 7681 --cwd %SYSTEMDRIVE%\ --writable %COMSPEC%
start code-tunnel.exe serve-web --without-connection-token --accept-server-license-terms --server-base-path vscode

:exit
popd
@echo on
