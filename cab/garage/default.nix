let 
    nixpkgsSrc = builtins.fetchTarball {
        name = "nixos-25.05-small";
        url = "https://github.com/NixOS/nixpkgs/archive/a5e9291e97f5ba0b4ba7d657ddedd5f86d11acfd.tar.gz";
        sha256 = "05q8vi2pgl91ya2nav2hi8zg4xy2vw90s6n3a567jfxbly885011";
    };
    pkgs = import nixpkgsSrc {};

in 
pkgs.mkShell {
    buildInputs = [
        pkgs.glibcLocales

        pkgs.git
        pkgs.cacert

        pkgs.cmake
        pkgs.gcc15

        pkgs.qt6.qtbase
        pkgs.qt6.wrapQtAppsHook
        pkgs.tomlplusplus
    ];

    shellHook = ''
        export LANG=C.UTF-8
        export LC_ALL=C.UTF-8
    '';
}
