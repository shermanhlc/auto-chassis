#!/usr/bin/env sh
set -e

NIX_IMAGE_TAG="nix-2.26.4_image:amd64"
DOCKERFILE="packer/dockerfile"
RUN_POST_BUILD=0

if [ "$1" == "--run" ]; then
    RUN_POST_BUILD=1
fi

if ! docker image inspect "$NIX_IMAGE_TAG" >/dev/null 2>&1; then
    echo "docker image $NIX_IMAGE_TAG not found... building new image"
    docker buildx build -f $DOCKERFILE -t "$NIX_IMAGE_TAG" .
fi


docker run --rm -it \
    -v "$(pwd)":/workspace \
    -w /workspace \
    "$NIX_IMAGE_TAG" \
    nix-shell /env/default.nix --run "/env/build"

if [ "$RUN_POST_BUILD" -eq 1 ]; then
    docker run --rm -it \
    -v "$(pwd)":/workspace \
    -w /workspace \
    "$NIX_IMAGE_TAG" \
    ./build/debug/bin/autochassis
fi
