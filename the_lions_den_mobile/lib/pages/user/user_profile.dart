import 'package:flutter/material.dart';
import 'package:flutter/src/foundation/key.dart';
import 'package:flutter/src/widgets/framework.dart';
import 'package:intl/intl.dart';
import 'package:provider/provider.dart';
import 'package:the_lions_den_mobile/model/user/user_response.dart';
import 'package:the_lions_den_mobile/pages/reservation/user_reservation_overview.dart';
import 'package:the_lions_den_mobile/pages/room/room_saved.dart';
import 'package:the_lions_den_mobile/pages/user/edit_profile.dart';
import 'package:the_lions_den_mobile/providers/user_provider.dart';
import 'package:the_lions_den_mobile/utils/auth_helper.dart';
import 'package:the_lions_den_mobile/widgets/master_screen.dart';

class UserProfile extends StatefulWidget {
  static const routeName = "/myProfile";
  const UserProfile({Key? key}) : super(key: key);

  @override
  State<UserProfile> createState() => _UserProfileState();
}

class _UserProfileState extends State<UserProfile> {
  UserProvider? _userProvider;
  UserResponse data = UserResponse();

  @override
  void initState() {
    _userProvider = context.read<UserProvider>();
    loadData();
    super.initState();
  }

  Future loadData() async {
    var tempData = await _userProvider?.getById(AuthHelper.user!.userId!);
    setState(() {
      data = tempData!;
    });
  }

  @override
  Widget build(BuildContext context) {
    return MasterScreenWidget(
      title: "MY PROFILE",
      child: _buildUserDetails(),
      selectedIndex: 3,
    );
  }

  _buildUserDetails() {
    if (data.fullName == null) {
      return Center(
        child: const CircularProgressIndicator(
          color: Colors.black,
        ),
      );
    }

    return Padding(
      padding: EdgeInsets.symmetric(horizontal: 25),
      child: ListView(
        children: [
          const SizedBox(height: 20.0),
          Container(
            height: 100,
            child: Center(
                child: Text(
              data.fullName!,
              style: TextStyle(
                  color: Colors.black,
                  fontSize: 40,
                  fontWeight: FontWeight.bold),
            )),
          ),
          //const SizedBox(height: 10.0),
          SizedBox(
              //width: screenWidthPercentage(context, percentage: 0.7),
              child: Text(data.username!,
                  style: TextStyle(
                      color: Colors.grey,
                      fontSize: 12,
                      fontWeight: FontWeight.w600))),
          const SizedBox(height: 40.0),
          Column(
            children: [
              Row(
                mainAxisAlignment: MainAxisAlignment.spaceEvenly,
                children: [
                  GestureDetector(
                    onTap: (() {
                      Navigator.pushNamed(
                          context, UserReservationOverview.routeName);
                    }),
                    child: Container(
                      height: 30,
                      width: 150,
                      child: Text("Reservatios",
                          style: TextStyle(
                              fontSize: 16, fontWeight: FontWeight.bold)),
                      alignment: Alignment.center,
                      decoration: BoxDecoration(
                          borderRadius: BorderRadius.circular(10),
                          gradient: const LinearGradient(colors: [
                            Color.fromARGB(255, 0, 69, 189),
                            Color.fromARGB(255, 10, 158, 227)
                          ])),
                    ),
                  ),
                  GestureDetector(
                    onTap: (() {
                      Navigator.pushNamed(context, SavedRooms.routeName);
                    }),
                    child: Container(
                      width: 150,
                      height: 30,
                      child: Text("Saved rooms",
                          style: TextStyle(
                              fontSize: 16, fontWeight: FontWeight.bold)),
                      alignment: Alignment.center,
                      decoration: BoxDecoration(
                          borderRadius: BorderRadius.circular(10),
                          gradient: const LinearGradient(colors: [
                            Color.fromARGB(255, 0, 69, 189),
                            Color.fromARGB(255, 10, 158, 227)
                          ])),
                    ),
                  )
                ],
              ),
            ],
          ),
          Column(
            children: [
              Padding(
                  padding: EdgeInsets.all(10),
                  child: Column(children: [
                    const SizedBox(height: 5.0),
                    TextFormField(
                      enabled: false,
                      initialValue: data.email,
                      decoration: InputDecoration(
                          border: InputBorder.none,
                          labelText: "Email",
                          icon: Icon(Icons.mail)),
                    ),
                    const SizedBox(height: 5.0),
                    TextFormField(
                      enabled: false,
                      initialValue: data.phoneNumber ?? "N/A",
                      decoration: InputDecoration(
                          border: InputBorder.none,
                          labelText: "Phone Number",
                          icon: Icon(Icons.phone)),
                    ),
                    const SizedBox(height: 5.0),
                    TextFormField(
                      enabled: false,
                      initialValue: data.gender ?? "N/A",
                      decoration: InputDecoration(
                          border: InputBorder.none,
                          labelText: "Gender",
                          icon: Icon(Icons.female_sharp)),
                    ),
                    TextFormField(
                      enabled: false,
                      initialValue: data.dateOfBirth != null
                          ? DateFormat('yyyy-MM-dd').format(data.dateOfBirth!)
                          : "",
                      decoration: InputDecoration(
                          border: InputBorder.none,
                          labelText: "Date of birth",
                          icon: Icon(Icons.calendar_today)),
                    ),
                  ])),
            ],
          ),
          const SizedBox(height: 60.0),
          GestureDetector(
            onTap: (() {
              Navigator.pushNamed(context, EditProfile.routeName);
            }),
            child: Container(
              width: double.infinity,
              height: 50,
              child: Text("Edit profile",
                  style: TextStyle(fontSize: 16, fontWeight: FontWeight.bold)),
              alignment: Alignment.center,
              decoration: BoxDecoration(
                  borderRadius: BorderRadius.circular(10),
                  gradient: const LinearGradient(colors: [
                    Color.fromARGB(255, 0, 69, 189),
                    Color.fromARGB(255, 10, 158, 227)
                  ])),
            ),
          ),
          const SizedBox(height: 15.0),
          const SizedBox(height: 30.0),
        ],
      ),
    );
  }
}
