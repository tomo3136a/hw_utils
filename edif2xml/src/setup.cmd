@pushd %~dp0
@powershell -Sta -NonInteractive -NoProfile -NoLogo -ExecutionPolicy RemoteSigned %~dpn0.ps1 %*
@popd
@pause
