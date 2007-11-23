all:
	gmcs -recurse:*.cs -pkg:gtk-sharp-2.0 -r:System.Drawing -pkg:gtk-dotnet-2.0 \
	-r:Data/MySql/MySql.Data.dll -r:System.Data -r:Npgsql.dll -pkg:gnome-sharp-2.0 \
	-pkg:gtkhtml-sharp-2.0 -t:library -out:RickiLib.dll \
	-resource:pixmaps/module.png \
	-resource:pixmaps/folder.png \
	-resource:pixmaps/dialog_auth_icon.png

test:
	make all
	gmcs hello.cs -r:RickiLib.dll -pkg:gtk-sharp-2.0
	mono hello.exe

data-testing:
	make all
	gmcs DataTesting.cs -r:RickiLib.dll -r:System.Data

draw:
	gmcs Canvas.cs -r:RickiLib.dll -pkg:gtk-sharp-2.0 -r:System.Drawing
	mono Canvas.exe

clean:
	rm -Rf *.dll *.exe *~

