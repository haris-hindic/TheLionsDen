import 'package:flutter/material.dart';
import 'package:the_lions_den_mobile/pages/reservation/user_reservation_overview.dart';
import 'package:the_lions_den_mobile/pages/room/room_overview.dart';
import 'package:the_lions_den_mobile/pages/room/room_saved.dart';
import 'package:the_lions_den_mobile/pages/user/user_profile.dart';
import 'package:the_lions_den_mobile/widgets/tld_appbar.dart';
import 'package:the_lions_den_mobile/widgets/tld_drawer.dart';

class MasterScreenWidget extends StatelessWidget {
  Widget? child;
  String? title;
  int? selectedIndex = 0;
  MasterScreenWidget({this.child, this.selectedIndex, this.title, Key? key})
      : super(key: key);

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: TLDAppbar(
        appBar: AppBar(),
        title: title!,
      ),
      drawer: TLDDrawer(),
      body: child!,
      bottomNavigationBar: BottomNavigationBar(
        items: const <BottomNavigationBarItem>[
          BottomNavigationBarItem(
            icon: Icon(Icons.home),
            label: 'Home',
          ),
          BottomNavigationBarItem(
            icon: Icon(Icons.monetization_on),
            label: 'Reservations',
          ),
          BottomNavigationBarItem(
            icon: Icon(Icons.favorite),
            label: 'Saved rooms',
          ),
          BottomNavigationBarItem(
            icon: Icon(Icons.person),
            label: 'My profile',
          )
        ],
        currentIndex: selectedIndex!,
        selectedItemColor: Colors.cyan,
        unselectedItemColor: Colors.black,
        onTap: (index) {
          switch (index) {
            case 0:
              Navigator.pushNamed(context, RoomOverview.routeName);
              break;
            case 3:
              Navigator.pushNamed(context, UserProfile.routeName);
              break;
            case 1:
              Navigator.pushNamed(context, UserReservationOverview.routeName);
              break;
            case 2:
              Navigator.pushNamed(context, SavedRooms.routeName);
              break;
          }
        },
      ),
    );
  }
}
