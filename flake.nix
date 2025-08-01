{
    description="CAB: a utility tool to develop, compile, and run work-in-progress programs with consistent dependencies and agnostic of the developer's machine and packages";

    inputs.nixpkgs.url="github:NixOS/nixpkgs/nixos-25.05-small";

    outputs = { self, nixpkgs }:
        let
            system="x86_64-linux";
            pkgs = import nixpkgs { inherit system; };
        in {
            packages.${system}.docker-image = pkgs.callPackage (self + "/cab/garage/image.nix") {};
        };
}