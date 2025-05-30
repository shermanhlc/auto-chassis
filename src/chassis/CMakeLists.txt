add_library(chassis STATIC
    include/firewall.h

    firewall.cpp
)

target_link_libraries(chassis
PRIVATE
    utils
)