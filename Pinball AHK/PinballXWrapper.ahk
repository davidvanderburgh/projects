#SingleInstance force
#NoEnv  ; Recommended for performance and compatibility with future AutoHotkey releases.
SendMode Input  ; Recommended for new scripts due to its superior speed and reliability.
SetWorkingDir %A_ScriptDir%  ; Ensures a consistent starting directory.

;-----Global variables-----

;GUI global variables
global ClockTime
global PlayTime = 0
global Volume
global PopUpText 
global TextBoxEntry

;Power button global variable
global PowerButtonPressTime

;-----Timer initialization-----

;Set a timer to run the "Idle" method every 30 seconds
SetTimer, Idle, 30000

;-----GUI initialization-----

;Initialize the Gui which will contain the popup text (volume, time, playtime)
SetFormat, Float, 2.0
SetTimer, PlayTimer, 60000
Gui, PopUpGui:new
Gui, PopUpGui:+AlwaysOnTop +ToolWindow -SysMenu -Caption
Gui, PopUpGui:Color, 000000
Gui, PopUpGui:Font, c39ff14 s46 q4, verdana
Gui, PopUpGui:Add, Picture, x0 y5 w90 h75, C:\volume.png
Gui, PopUpGui:Add, Text, vTextBoxEntry w1980 x110 y0 BackgroundTrans,


;-----Button events-----

;Volume Up button press
$Joy2:: 
{
	SoundSet +3
	ShowPopUp()
	return
}

;Volume Down button press
$Joy13::
{
	SoundSet -3
	ShowPopUp()
	return
}

;Power button press
$Joy5::
{
	;Make a timer which checks if the power button is still held 500ms later (negative so it fires only once)
	SetTimer, PowerButtonTimer, -500
}

;-----Helper functions-----

;Pops up a gui on the backglass with the volume, current time and play time
ShowPopUp()
{
	SoundGet, Volume
	UpdatePopUpText()

	GuiControl, PopUpGui:Text, TextBoxEntry, %PopUpText%

	Gui, PopUpGui:Show, NA w1920 h100 x-1920 y980, PopUpGuiWindow

	SetTimer, ClosePopUp, -5000 ; negative so it only fires once
	return
}

;Update the text in the popup to the current volume, time, and playtime
UpdatePopUpText()
{
	FormatTime, ClockTime,, h:mm tt
	PopUpText = %Volume%  | %ClockTime%  |  You have been playing for %PlayTime% minutes
}

;Closes the popup gui
ClosePopUp:
{
	;allow the GUI to be addressed when transparent (hidden)
	detecthiddenwindows, on

	;start the transparency of the GUI at full opaque (255)
	transparencyValue := 255

	;the amount to change the transparency down after each loop
	transparencyChangeFactor := 4

	;loop a total of the starting transparency value divided by the change factor (integer division is "//") 
	loopAmount := transparencyValue//transparencyChangeFactor

	Loop, %loopAmount%
	{
		;calculate the new transparency value as the current one minus the change factor
		transparencyValue := transparencyValue - transparencyChangeFactor

		;update the GUI window with the latest transparency
		WinSet, Transparent, %transparencyValue%, PopUpGuiWindow

		;wait 15ms in between transparency changes
		Sleep, 15
	} 	

	;hide the GUI window
	Gui, PopUpGui:submit

	;reset the window transparency back to full opaque for the next time the window pops up (GUI remains hidden)
	WinSet, Transparent, 255, PopUpGuiWindow

	return
}

;Increments the play time by 1 minute
PlayTimer:
{
	PlayTime += 1
	return
}

;Checks to see if the power button is held for at least 500ms and if so initiates shutdown
PowerButtonTimer:
{
	;If the power button is still pressed after 500ms
	; (this logic does not account for multiple power button pushes and will trigger if the button is on at both ends of any 500ms interval)
	if GetKeyState("Joy5")
	{		
		;Shut down the system
		ShutDownSequence()
	}
	;If the power button is in the released state
	else
	{	
		SoundPlay, C:\hold.mp3
	}
	return
}

;Method to elegantly shutdown PinballX and VPX
ShutDownSequence()
{
	;Play an audio file indicating shutdown sequence
	SoundPlay, C:\shutdownMP3.mp3

	;Elegantly quit from Visual Pinball (saves high score)
	IfWinExist, Visual Pinball
	{
		WinActivate
		SendInput Q
	
		;wait for pinballx to gain focus (this is necessary!)
		Sleep, 6000
	}
	
	;close Pinballx (does not receive signals from SendInput)
	;this will save the state of the last pinball table played
	PostMessage, 0x112, 0xF060,,, PinballX
	
	;Give some time for the "Goodbye" PinballX audio to play
	Sleep, 2500

	;Shutdown the computer
	Shutdown, 1
	
	return
}

Idle:
{
	;If no button pushes for 15 minutes (900000ms) then shut down the system
	if (A_TimeIdle > 900000)
	{
		ShutDownSequence()
	}
	;One minute before inactivity shutdown, play a warning message (timer is run every 30 seconds so one 30 second window is given)
	else if (A_TimeIdle > 840000 && A_TimeIdle < 870001)
	{
		SoundPlay, C:\inactivity.mp3
	}
	return
}
