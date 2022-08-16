import 'dart:ui';

import 'package:flutter/material.dart';
import 'package:flutter/src/foundation/key.dart';
import 'package:flutter/src/widgets/framework.dart';
import 'package:the_lions_den_mobile/pages/room/room_details.dart';
import 'package:the_lions_den_mobile/pages/room/room_overview.dart';

class TLDBottomNavigation extends StatefulWidget {
  const TLDBottomNavigation({Key? key}) : super(key: key);

  @override
  State<TLDBottomNavigation> createState() => _TLDBottomNavigationState();
}

class _TLDBottomNavigationState extends State<TLDBottomNavigation> {
  int _selectedIndex = 0;

  void _onItemTapped(int index) {
    setState(() {
      _selectedIndex = index;
    });

    if (_selectedIndex == 0) {
      Navigator.pushNamed(context, RoomOverview.routeName);
    } else if (_selectedIndex == 1) {
      Navigator.pushNamed(context, RoomDetails.routeName);
    }
  }

  @override
  Widget build(BuildContext context) {
    return BottomNavigationBar(
      items: const <BottomNavigationBarItem>[
        BottomNavigationBarItem(
          icon: Icon(Icons.home),
          label: 'Rooms',
        ),
        BottomNavigationBarItem(
          icon: Icon(Icons.business),
          label: 'Business',
        ),
      ],
      currentIndex: _selectedIndex,
      selectedItemColor: Colors.amber[800],
      onTap: _onItemTapped,
    );
  }
}
