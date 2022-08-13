import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:the_lions_den_mobile/pages/room/room_overview.dart';
import 'package:the_lions_den_mobile/pages/user/login.dart';
import 'package:the_lions_den_mobile/utils/auth_helper.dart';

class TLDDrawer extends StatelessWidget {
  TLDDrawer({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return Drawer(
      child: ListView(
        children: [
          ListTile(
            title: Text('Rooms'),
            onTap: () {
              Navigator.pushNamed(context, RoomOverview.routeName);
            },
          ),
          ListTile(
            title: Text('Log out'),
            onTap: () {
              AuthHelper.password = null;
              AuthHelper.username = null;
              Navigator.popAndPushNamed(context, Login.routeName);
            },
          ),
        ],
      ),
    );
  }
}
