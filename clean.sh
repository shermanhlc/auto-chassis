#!/usr/bin/env sh
set -e

if [ -d "build" ]; then
    rm -rf build

    echo "Build cleaned"
else
    echo "Nothing to clean, no build directory"
fi
