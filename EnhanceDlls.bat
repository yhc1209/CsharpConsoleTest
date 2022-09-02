@ECHO OFF
CALL:ParamCheck %1 %2
SET SNK_FILE=%1

:loop
SHIFT
IF [%1]==[] GOTO done
CALL:EnhanceDll %1 %SNK_FILE%
GOTO loop

:done
echo EnhanceDlls.bat completed successfully!
GOTO:EOF

:: -----------------------------------------------------------
:: --------- Enhance DLL                             ---------
:: -----------------------------------------------------------
:EnhanceDll
IF NOT EXIST %1 ECHO the DLL file '%1' does not exist && EXIT
ildasm /all /out=weak.il %1
ilasm /dll /key=%2 /res=weak.res weak.il /quiet
:: ------ replace weak dll  ------
DEL %1
MOVE weak.dll %1 > NUL
:: ------ clean clear       ------
DEL *.resources
DEL weak.*
@REM :: ------ check how strong it is ------
@REM sn -T %1
GOTO:EOF

:: -----------------------------------------------------------
:: --------- Enhance DLL simulation                  ---------
:: -----------------------------------------------------------
:EnhanceDll_test
@REM IF NOT EXIST %1 ECHO %1 does not exist && EXIT
ECHO enhance %1 with %2
GOTO:EOF

:: -----------------------------------------------------------
:: --------- Parameters Check                        ---------
:: -----------------------------------------------------------
:ParamCheck
IF [%1]==[] ECHO more parameters is required. && EXIT
IF "%1"=="-h" CALL:HelpText && GOTO:EOF
IF "%1"=="--help" CALL:HelpText && GOTO:EOF
IF [%2]==[] ECHO more parameters is required. && EXIT
IF NOT EXIST %1 ECHO the SNK file '%1' does not exist! && EXIT
GOTO:EOF

:: -----------------------------------------------------------
:: --------- Help text                               ---------
:: -----------------------------------------------------------
:HelpText
ECHO - EnhanceDlls.bat Intro. -
ECHO    EnhanceDlls.bat can help you to enhance your DLL file from a normal DLL
ECHO    to a stronger DLL (the DLL file with strong name).
ECHO.
ECHO    -h, --help     To show this introduction.
ECHO.
ECHO Usage:
ECHO    EnhanceDlls.bat [SNK file] [DLL file 1] [DLL file 2]...
ECHO.
GOTO:EOF