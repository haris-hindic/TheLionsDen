import 'dart:ui';

import 'package:flutter/material.dart';
import 'package:flutter/src/foundation/key.dart';
import 'package:flutter/src/widgets/framework.dart';
import 'package:the_lions_den_mobile/pages/reservation/user_reservation_overview.dart';
import 'package:the_lions_den_mobile/pages/room/room_details.dart';
import 'package:the_lions_den_mobile/pages/room/room_overview.dart';
import 'package:the_lions_den_mobile/pages/room/room_saved.dart';
import 'package:the_lions_den_mobile/pages/user/user_profile.dart';

class TLDBottomNavigation extends StatefulWidget {
  const TLDBottomNavigation({Key? key}) : super(key: key);

  @override
  State<TLDBottomNavigation> createState() => _TLDBottomNavigationState();
}

class _TLDBottomNavigationState extends State<TLDBottomNavigation> {
  int? index = 0;

  @override
  Widget build(BuildContext context) {
    return BottomNavigationBar(
      items: const <BottomNavigationBarItem>[
        BottomNavigationBarItem(
          icon: Icon(Icons.home),
          label: 'Home',
        ),
        BottomNavigationBarItem(
          icon: Icon(Icons.person),
          label: 'My profile',
        ),
        BottomNavigationBarItem(
          icon: Icon(Icons.monetization_on),
          label: 'Reservations',
        ),
        BottomNavigationBarItem(
          icon: Icon(Icons.favorite),
          label: 'Saved rooms',
        )
      ],
      currentIndex: index!,
      selectedItemColor: Colors.cyan,
      unselectedItemColor: Colors.black,
      onTap: (index) {
        switch (index) {
          case 0:
            Navigator.pushNamed(context, RoomOverview.routeName);
            break;
          case 1:
            Navigator.pushNamed(context, UserProfile.routeName);
            break;
          case 2:
            Navigator.pushNamed(context, UserReservationOverview.routeName);
            break;
          case 3:
            Navigator.pushNamed(context, SavedRooms.routeName);
            break;
        }
      },
    );
  }
}
