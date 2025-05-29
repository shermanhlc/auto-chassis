cmake_minimum_required(VERSION 3.31)

set(PROJECT_NAME autochassis)
project(${PROJECT_NAME} VERSION 1.0.0)

set(CMAKE_CXX_STANDARD 17)
set(CMAKE_CXX_STANDARD_REQUIRED true)

# moves binary output to build/bin
set(CMAKE_RUNTIME_OUTPUT_DIRECTORY ${CMAKE_BINARY_DIR}/bin)


# packages
find_package(Qt6 REQUIRED COMPONENTS Core)

add_subdirectory(src)
