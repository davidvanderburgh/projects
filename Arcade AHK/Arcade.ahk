#SingleInstance force
#InstallKeybdHook
#NoEnv  ; Recommended for performance and compatibility with future AutoHotkey releases.
; #Warn  ; Enable warnings to assist with detecting common errors.
SendMode Input  ; Recommended for new scripts due to its superior speed and reliability.
SetWorkingDir %A_ScriptDir%  ; Ensures a consistent starting directory.

; Set a timer to run Idle every 30 seconds (used to shutdown after 15mins of inactivity)
SetTimer, Idle, 30000

Idle:
{
	; If no buttons are pushed for 15 minutes, shutdown the system
	if (A_TimeIdleKeyboard > 900000)
	{
		Shutdown, 1	
	}
	return	
}

;Create variables visible to all functions
global ClockTime
global PlayTime = 0
global Volume
global PopUpText 
global TextBoxEntry
global CloseSplash = false

;Initialize the Gui which will contain the popup text (volume, time, playtime)
InitializeGui()

;turn the system mouse cursor off (hide it until ` is pressed)
SystemCursor("OFF")

; show "Loading" splash screen until loaded
ShowSplashScreen()

;Close the splash screen manually (~ to pass through tab key to any other program)
~Tab::
{
	CloseSplash = true
	return
}

ShowSplashScreen()
{
	SplashImage, C:\Users\david\Pictures\feload.png, b w1366 h768 x0 y0
	Loop
	{
		; Look for LaunchBox window.  Once it is found, break the loop and turn off the splash image
		IfWinExist, ahk_exe BigBox.exe
		{	
			break
		}
		;if Tab is pressed, CloseSplash turns to true (useful during debugging)
		if (CloseSplash)
		{
			CloseSplash = false
			break
		}
		; Poll every 100ms
		Sleep, 100
	}
	SplashImage, Off
	return
}


;"Volume Down" button
$F3:: 
{
	SoundSet -3
	ShowPopUp()
	return
}

;"Volume Up" button
$F4:: 
{
	SoundSet +3
	ShowPopUp()
	return
}

;"Controls?" button (set as pass-through so LEDBlinky can register it and speak commands while LaunchBox FE is active)
~Home:: 
{
	WinGet pid, PID, A

	; Get WMI service object.
	wmi := ComObjGet("winmgmts:")

	; Run query to retrieve matching process(es).
	queryEnum := wmi.ExecQuery(""
		. "Select * from Win32_Process where ProcessId=" . pid)
		._NewEnum()

	if queryEnum[process]
	{
		CommandLineString = % process.CommandLine
		EmuHawkRunning = false
		DolphinRunning = false
		PinballFX2Running = false
		MameRunning = false		

		IfInString, CommandLineString, EmuHawk
			EmuHawkRunning = true
		IfInString, CommandLineString, Dolphin
			DolphinRunning = true
		IfInString, CommandLineString, Pinball FX2
			PinballFX2Running = true
		IfInString, CommandLineString, mame64
			MameRunning = true		

		if (%EmuHawkRunning% || %DolphinRunning% || %PinballFX2Running%)
		{
			;split line delimited by " use 2nd to last entry to pass to LEDBlinky			
			WordArray := StrSplit(CommandLineString, """")
    			RomName = WordArray[WordArray.MaxIndex()-1]			
			Run, C:\LEDBlinky\LEDBlinky.exe %RomName%
		}
		else if (%MameRunning%)
		{
			;split line delimited by a space, use last entry to pass to LEDBlinky
			WordArray := StrSplit(CommandLineString, A_Space)
			RomName := WordArray[WordArray.MaxIndex()]			
			Run, C:\LEDBlinky\LEDBlinky.exe %RomName%
		}
	}
	return
}

;Restore cursor
$`::
{
	SystemCursor("Toggle")
	return
}

InitializeGui()
{
	SetFormat, Float, 2.0
	SetTimer, PlayTimer, 60000

	Gui, PopUpGui:new
	Gui, PopUpGui:+AlwaysOnTop +ToolWindow -SysMenu -Caption
	Gui, PopUpGui:Color, 000000
	Gui, PopUpGui:Font, c39ff14 s32 q4, verdana

	Gui, PopUpGui:Add, Picture, x0 y5 w64 h53, C:\volume.png
	Gui, PopUpGui:Add, Text, vTextBoxEntry w1366 x78 y0 BackgroundTrans,
	return
}

;Method to pop up a gui on the backglass with the volume, current time and play time
ShowPopUp()
{
	SoundGet, Volume
	UpdatePopUpText()

	GuiControl, PopUpGui:Text, TextBoxEntry, %PopUpText%

	Gui, PopUpGui:Show, NA w1366 h71 x0 y697, PopUpGuiWindow

	SetTimer, ClosePopUp, -5000 ; negative so it only fires once
	return
}

;Update the text in the popup to the current volume, time, and playtime
UpdatePopUpText()
{
	FormatTime, ClockTime,, h:mm tt
	PopUpText = %Volume%  | %ClockTime%  |  You have been playing for %PlayTime% minutes
	return
}

;Closes the popup gui
ClosePopUp:
{
	settitlematchmode, 3
	detecthiddenwindows, on

	FADE := 255
	Loop 61
	{
		FADE := FADE - 4
		WinSet, Transparent, %FADE%, PopUpGuiWindow

		if Fade=5
		{
			winshow, PopUpGuiWindow
			winactivate, PopUpGuiWindow
		}
		Sleep, 15
	}
	WinSet, Transparent, 0, PopUpGuiWindow

	Gui, PopUpGui:submit
	WinSet, Transparent, 255, PopUpGuiWindow

	return
}

;Increments the play time by 1 minute
PlayTimer:
{
	PlayTime += 1
	return
}

SystemCursor(OnOff=1)   ; INIT = "I","Init"; OFF = 0,"Off"; TOGGLE = -1,"T","Toggle"; ON = others
{
    static AndMask, XorMask, $, h_cursor
        ,c0,c1,c2,c3,c4,c5,c6,c7,c8,c9,c10,c11,c12,c13 ; system cursors
        , b1,b2,b3,b4,b5,b6,b7,b8,b9,b10,b11,b12,b13   ; blank cursors
        , h1,h2,h3,h4,h5,h6,h7,h8,h9,h10,h11,h12,h13   ; handles of default cursors
    if (OnOff = "Init" or OnOff = "I" or $ = "")       ; init when requested or at first call
    {
        $ = h                                          ; active default cursors
        VarSetCapacity( h_cursor,4444, 1 )
        VarSetCapacity( AndMask, 32*4, 0xFF )
        VarSetCapacity( XorMask, 32*4, 0 )
        system_cursors = 32512,32513,32514,32515,32516,32642,32643,32644,32645,32646,32648,32649,32650
        StringSplit c, system_cursors, `,
        Loop %c0%
        {
            h_cursor   := DllCall( "LoadCursor", "Ptr",0, "Ptr",c%A_Index% )
            h%A_Index% := DllCall( "CopyImage", "Ptr",h_cursor, "UInt",2, "Int",0, "Int",0, "UInt",0 )
            b%A_Index% := DllCall( "CreateCursor", "Ptr",0, "Int",0, "Int",0
                , "Int",32, "Int",32, "Ptr",&AndMask, "Ptr",&XorMask )
        }
    }
    if (OnOff = 0 or OnOff = "Off" or $ = "h" and (OnOff < 0 or OnOff = "Toggle" or OnOff = "T"))
        $ = b  ; use blank cursors
    else
        $ = h  ; use the saved cursors

    Loop %c0%
    {
        h_cursor := DllCall( "CopyImage", "Ptr",%$%%A_Index%, "UInt",2, "Int",0, "Int",0, "UInt",0 )
        DllCall( "SetSystemCursor", "Ptr",h_cursor, "UInt",c%A_Index% )
    }
}