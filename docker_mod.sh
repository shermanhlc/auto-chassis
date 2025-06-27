#!/usr/bin/env sh
set -e

RUN_POST_BUILD=0
if [[ "$1" == "--run" ]]; then
    RUN_POST_BUILD=1
fi

docker run --rm -it \
    -v "$(pwd)":/workspace \
    -w /workspace \
    nix-2.26.4_image:amd64 \
    nix-shell /env/default.nix --run "/env/build"

if [[ $RUN_POST_BUILD -eq 1 ]]; then
    docker run --rm -it \
    -v "$(pwd)":/workspace \
    -w /workspace \
    nix-2.26.4_image:amd64 \
    ./build/debug/bin/autochassis
fi
