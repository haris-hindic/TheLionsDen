import 'package:flutter/material.dart';
import 'package:provider/provider.dart';
import 'package:the_lions_den_mobile/pages/room/room_overview.dart';
import 'package:the_lions_den_mobile/pages/user/login.dart';
import 'package:the_lions_den_mobile/pages/user/registration.dart';
import 'package:the_lions_den_mobile/providers/room_provider.dart';
import 'package:the_lions_den_mobile/providers/room_type_provider.dart';
import 'package:the_lions_den_mobile/providers/user_provider.dart';

void main() {
  runApp(MultiProvider(providers: [
    ChangeNotifierProvider(create: (_) => RoomProvider()),
    ChangeNotifierProvider(create: (_) => UserProvider()),
    ChangeNotifierProvider(create: (_) => RoomTypeProvider()),
  ], child: const MyApp()));
}

class MyApp extends StatelessWidget {
  const MyApp({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      debugShowCheckedModeBanner: true,
      title: "The Lion's Den",
      theme: ThemeData(
        primarySwatch: Colors.blue,
      ),
      home: Login(),
      onGenerateRoute: (settings) {
        if (settings.name == RoomOverview.routeName) {
          return MaterialPageRoute(builder: (context) => RoomOverview());
        }
        if (settings.name == Registration.routeName) {
          return MaterialPageRoute(builder: (context) => Registration());
        }
        if (settings.name == Login.routeName) {
          return MaterialPageRoute(builder: (context) => Login());
        }
      },
    );
  }
}
