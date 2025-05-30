add_library(utils STATIC
    include/utils/point.h
    include/utils/equations.h

    equations.cpp
)

target_include_directories(utils
PUBLIC
    ${CMAKE_CURRENT_SOURCE_DIR}/include
)