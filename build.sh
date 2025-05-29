#!/usr/bin/env sh
set -e

CALLED_PATH=$(pwd)

if [ ! -d "build" ]; then
    mkdir build
fi

cd build
cmake ..
cmake --build .

cd "$CALLED_PATH"
echo "Build complete"
