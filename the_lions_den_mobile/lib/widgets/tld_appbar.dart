import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';

class TLDAppbar extends StatelessWidget implements PreferredSizeWidget {
  TLDAppbar({Key? key, required this.title, required this.appBar})
      : super(key: key);

  final String title;
  final AppBar appBar;
  @override
  Widget build(BuildContext context) {
    return AppBar(
      foregroundColor: Colors.black,
      backgroundColor: Colors.transparent,
      elevation: 0,
      centerTitle: true,
      title: Text(
        title,
        style: TextStyle(
            fontSize: 18.0, fontWeight: FontWeight.normal, color: Colors.black),
      ),
    );
  }

  @override
  Size get preferredSize => new Size.fromHeight(appBar.preferredSize.height);
}
