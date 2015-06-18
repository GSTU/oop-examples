# makefile : The program makefile
# Copyright (C) 2001-2002  Paulo Pinto
#
# This library is free software; you can redistribute it and/or
# modify it under the terms of the GNU Lesser General Public
# License as published by the Free Software Foundation; either
# version 2 of the License, or (at your option) any later version.
#
# This library is distributed in the hope that it will be useful,
# but WITHOUT ANY WARRANTY; without even the implied warranty of
# MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
# Lesser General Public License for more details.
#
# You should have received a copy of the GNU Lesser General Public
# License along with this library; if not, write to the
# Free Software Foundation, Inc., 59 Temple Place - Suite 330,
# Boston, MA 02111-1307, USA.


# C# command line compiler
CSC=csc.exe
HHC=hhc.exe

# Target names
CHECKERS_CTRL=CheckersCtrl.dll
CHECKERS=Checkers.exe
RESOURCE=AboutBox.resources
HELP=Checkers.chm

# Target directory
TARGET_DIR=..\bin

#Set some common flags
!IFDEF DEBUG
DEBUG=/debug+ /d:DEBUG
EXE_FLAGS=/t:exe $(DEBUG)
!ELSE
DEBUG=
EXE_FLAGS=/t:winexe
!ENDIF

COPY_FLAGS=/Y /I /R
DLL_FLAGS=/t:library $(DEBUG)
REFERENCES=/reference:$(CHECKERS_CTRL)
RESOURCES=/resource:AboutBox.resources

# Default rule
all: $(CHECKERS_CTRL) $(RESOURCE) $(CHECKERS)  helpfile


# Default rule with no help file
nohelp: $(CHECKERS_CTRL) $(RESOURCE) $(CHECKERS)

# Install the application
install:
	-mkdir $(TARGET_DIR)
	xcopy $(COPY_FLAGS) $(CHECKERS_CTRL) $(TARGET_DIR)
	xcopy $(COPY_FLAGS) $(CHECKERS) $(TARGET_DIR)
	xcopy $(COPY_FLAGS) help\$(HELP) $(TARGET_DIR)

# Build the Windows Form program
$(CHECKERS): Checkers.cs AboutBox.cs
	$(CSC) $(EXE_FLAGS) /out:$@ $** $(REFERENCES) $(RESOURCES)

# Convert the resource file
$(RESOURCE): AboutBox.resx
	resgen AboutBox.resx AboutBox.resources

# Build the assembly with all the code logic
$(CHECKERS_CTRL): BadCoord.cs BadMoveException.cs BoardView.cs CheckersBoard.cs Computer.cs List.cs Move.cs
	$(CSC) $(DLL_FLAGS) /out:$@ $**

helpfile: help\Checkers.hhp
	-$(HHC) help\Checkers.hhp
	echo > helpfile

clean: 
	@-del /Q *.obj *.tlb *.pdb *.exe *.dll *.*~ *.resources helpfile help\*.chm
















