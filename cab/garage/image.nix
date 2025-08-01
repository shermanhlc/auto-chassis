{ pkgs }:

pkgs.dockerTools.buildImage {
    name = "dt_nix-25.05_image";
    tag = "amd64";

    copyToRoot = [
        (pkgs.runCommand "setup_opt" {} ''
            mkdir -p $out/opt
            cp -r ${../toolkit} $out/opt/toolkit
            chmod +x $out/opt/toolkit/build
        '')

        pkgs.nix
        pkgs.curl  # for testings purposes
        pkgs.cacert
        pkgs.coreutils
        pkgs.bashInteractive
    ];

    runAsRoot = ''
        ${pkgs.dockerTools.shadowSetup}

        groupadd -g 1000 sherman
        useradd -m -u 1000 -g 1000 sherman
        mkdir /workbench
        chown -R sherman:sherman /workbench
        chown -R sherman:sherman /tmp
    '';

    created="now";
    config = {
        WorkingDir="/workbench";
        User="sherman";
        Cmd=[ "bash" ];
    };
}