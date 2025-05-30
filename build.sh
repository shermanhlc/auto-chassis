#!/usr/bin/env sh
set -e

if [ ! -d "build" ]; then
    mkdir -p build/debug
    mkdir -p build/release
fi

cmake -S . -B build/debug -DCMAKE_BUILD_TYPE=Debug
cmake --build build/debug

echo "Build complete"
