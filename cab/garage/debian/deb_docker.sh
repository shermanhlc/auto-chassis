#!/usr/bin/env sh
set -e

# # user
# USER_UID=$(id -u)
# USER_GID=$(id -g)

# NIX_IMAGE_TAG="nix-2.26.4_image:amd64"
# DOCKERFILE="cab/garage/dockerfile"
# RUN_POST_BUILD=0

# if [ "$1" == "--run" ]; then
#     RUN_POST_BUILD=1
# fi

# if ! docker image inspect "$NIX_IMAGE_TAG" >/dev/null 2>&1; then
#     echo "docker image \"$NIX_IMAGE_TAG\" not found... building new image"
#     docker buildx build -f $DOCKERFILE -t "$NIX_IMAGE_TAG" .
# fi


# docker run --rm -it \
#     # -u "$USER_UID":"$USER_GID" \
#     -v "$(pwd)":/workbench \
#     -w /workbench \
#     "$NIX_IMAGE_TAG" \
#     nix-shell /env/default.nix --run "/env/toolkit/build"

# if [ "$RUN_POST_BUILD" -eq 1 ]; then
#     docker run --rm -it \
#         -u "$USER_UID":"$USER_GID" \
#         -v "$(pwd)":/workbench \
#         -w /workbench \
#         "$NIX_IMAGE_TAG" \
#         nix-shell /env/default.nix --run "./build/debug/bin/autochassis"
# fi
docker run --rm -it \
    -v "$(pwd)":/workbench \
    -w /workbench \
    "debian_image:amd64" \
    /env/toolkit/build
