import 'package:flutter/material.dart';
import 'package:provider/provider.dart';
import 'package:the_lions_den_mobile/pages/room/room_overview.dart';
import 'package:the_lions_den_mobile/pages/user/registration.dart';
import 'package:the_lions_den_mobile/providers/user_provider.dart';
import 'package:the_lions_den_mobile/utils/auth_helper.dart';

class Login extends StatelessWidget {
  static const routeName = "/login";
  //final String title;

  TextEditingController _usernameController = new TextEditingController();
  TextEditingController _passwordController = new TextEditingController();

  late UserProvider _userProvider;

  Login({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    _userProvider = Provider.of<UserProvider>(context, listen: false);
    return Scaffold(
      // appBar: AppBar(
      //   title: Text(this.title),
      // ),
      body: SingleChildScrollView(
        child: Column(
          children: [
            Container(
              height: 400,
              decoration: const BoxDecoration(
                  image: DecorationImage(
                      image: AssetImage("assets/images/ritz.jpg"),
                      fit: BoxFit.fill)),
              child: Stack(children: [
                Container(
                  height: 200,
                  child: const Center(
                      child: Text(
                    "The Lion's Den",
                    style: TextStyle(
                        color: Colors.white,
                        fontSize: 40,
                        fontWeight: FontWeight.bold),
                  )),
                ),
              ]),
            ),
            Padding(
                padding: const EdgeInsets.all(48),
                child: Container(
                  decoration: BoxDecoration(
                      color: Colors.white,
                      borderRadius: BorderRadius.circular(20)),
                  child: Column(children: [
                    Container(
                        padding: const EdgeInsets.all((8)),
                        decoration: const BoxDecoration(
                            border:
                                Border(bottom: BorderSide(color: Colors.grey))),
                        child: TextField(
                          controller: _usernameController,
                          decoration: InputDecoration(
                              border: InputBorder.none,
                              hintText: "Username",
                              hintStyle: TextStyle(color: Colors.grey)),
                        )),
                    Container(
                        padding: const EdgeInsets.all((8)),
                        child: TextField(
                          controller: _passwordController,
                          obscureText: true,
                          decoration: InputDecoration(
                            border: InputBorder.none,
                            hintText: "Password",
                            hintStyle: TextStyle(color: Colors.grey),
                          ),
                        )),
                  ]),
                )),
            Container(
              height: 50,
              width: 300,
              decoration: BoxDecoration(
                  borderRadius: BorderRadius.circular(10),
                  gradient: const LinearGradient(colors: [
                    Color.fromARGB(255, 0, 69, 189),
                    Color.fromARGB(255, 10, 158, 227)
                  ])),
              child: InkWell(
                child: const Center(child: Text("Login")),
                onTap: () async {
                  try {
                    AuthHelper.username = _usernameController.text;
                    AuthHelper.password = _passwordController.text;

                    await _userProvider.login();

                    await Navigator.pushNamed(context, RoomOverview.routeName);
                  } on Exception catch (e) {
                    showDialog(
                        context: context,
                        builder: (BuildContext context) => AlertDialog(
                              title: Text("Error"),
                              content: Text("Wrong username or password!"),
                              actions: [
                                TextButton(
                                    onPressed: () => Navigator.pop(context),
                                    child: Text("Ok"))
                              ],
                            ));
                  }
                },
              ),
            ),
            const SizedBox(
              height: 15,
            ),
            Container(
              child: InkWell(
                child: const Center(child: Text("Register")),
                onTap: () {
                  Navigator.pushNamed(context, Registration.routeName);
                },
              ),
            ),
            const SizedBox(
              height: 20,
            ),
            Padding(
              padding: const EdgeInsets.all(8.0),
              child: Text(
                "By logging in you agree to our terms, conditions and privacy policy.",
                textAlign: TextAlign.center,
                style:
                    TextStyle(fontWeight: FontWeight.w400, color: Colors.grey),
              ),
            ),
            const SizedBox(
              height: 10,
            ),
          ],
        ),
      ),
    );
  }
}
