import 'package:flutter/material.dart';
import 'package:flutter/src/foundation/key.dart';
import 'package:flutter/src/widgets/framework.dart';
import 'package:intl/intl.dart';
import 'package:provider/provider.dart';
import 'package:the_lions_den_mobile/model/user/user_insert_request.dart';
import 'package:the_lions_den_mobile/pages/user/login.dart';
import 'package:the_lions_den_mobile/providers/user_provider.dart';

class Registration extends StatefulWidget {
  static const routeName = "/register";

  Registration({Key? key}) : super(key: key);

  @override
  State<Registration> createState() => _RegistrationState();
}

class _RegistrationState extends State<Registration> {
  final TextEditingController _firstNameController = TextEditingController();
  final TextEditingController _lastNameController = TextEditingController();
  final TextEditingController _usernameController = TextEditingController();
  final TextEditingController _passwordController = TextEditingController();
  final TextEditingController _passwordConfirmationController =
      TextEditingController();
  final TextEditingController _emailController = TextEditingController();
  final TextEditingController _phoneNumberController = TextEditingController();
  final TextEditingController _genderController = TextEditingController();
  final TextEditingController _dateOfBirthController = TextEditingController();

  late UserProvider _userProvider;

  @override
  void initState() {
    _dateOfBirthController.text = ""; //set the initial value of text field
    super.initState();
  }

  @override
  Widget build(BuildContext context) {
    _userProvider = Provider.of<UserProvider>(context, listen: false);
    return Scaffold(
        body: Padding(
      padding: EdgeInsets.symmetric(horizontal: 25),
      child: ListView(
        children: [
          const SizedBox(height: 20.0),
          Container(
            height: 125,
            decoration: const BoxDecoration(
                image: DecorationImage(
                    image: AssetImage("assets/images/logo.png"),
                    fit: BoxFit.fill)),
          ),
          const SizedBox(height: 10.0),
          SizedBox(
              width: screenWidthPercentage(context, percentage: 0.7),
              child: Text("Enter your credentials, before you start.",
                  style: TextStyle(
                      color: Colors.grey,
                      fontSize: 12,
                      fontWeight: FontWeight.w600))),
          const SizedBox(height: 40.0),
          Column(
            children: [
              Padding(
                padding: EdgeInsets.all(10),
                child: Column(children: [
                  TextFormField(
                    controller: _firstNameController,
                    decoration: InputDecoration(
                        labelText: "First Name", icon: Icon(Icons.person)),
                    // validator: (String? value) {
                    //   if (value == null || value.isEmpty) {
                    //     return 'This field is required!';
                    //   }
                    //   return null;
                    // },
                  ),
                  const SizedBox(height: 5.0),
                  TextField(
                    decoration: InputDecoration(
                        labelText: "Last Name", icon: Icon(Icons.person)),
                    controller: _lastNameController,
                  ),
                  const SizedBox(height: 5.0),
                  TextField(
                    decoration: InputDecoration(
                        labelText: "Email", icon: Icon(Icons.mail)),
                    controller: _emailController,
                  ),
                  const SizedBox(height: 5.0),
                  TextField(
                    decoration: InputDecoration(
                        labelText: "Phone Number", icon: Icon(Icons.phone)),
                    controller: _phoneNumberController,
                  ),
                  const SizedBox(height: 5.0),
                  TextField(
                    decoration: InputDecoration(
                        labelText: "Username", icon: Icon(Icons.person)),
                    controller: _usernameController,
                  ),
                  const SizedBox(height: 5.0),
                  TextField(
                    decoration: InputDecoration(
                        labelText: "Password", icon: Icon(Icons.password)),
                    obscureText: true,
                    controller: _passwordController,
                  ),
                  const SizedBox(height: 5.0),
                  TextField(
                    decoration: InputDecoration(
                        labelText: "Password Confirmation",
                        icon: Icon(Icons.password)),
                    obscureText: true,
                    controller: _passwordConfirmationController,
                  ),
                  TextField(
                    decoration: InputDecoration(
                        labelText: "Gender", icon: Icon(Icons.female_sharp)),
                    controller: _genderController,
                  ),
                  TextField(
                    controller: _dateOfBirthController,
                    decoration: InputDecoration(
                        labelText: "Date of birth",
                        icon: Icon(Icons.calendar_today)),
                    onTap: () async {
                      DateTime? pickedDate = await showDatePicker(
                          context: context,
                          initialDate: DateTime.now(),
                          firstDate: DateTime(1950),
                          lastDate: DateTime(2100));

                      if (pickedDate != null) {
                        String formattedDate =
                            DateFormat('yyyy-MM-dd').format(pickedDate);

                        setState(() {
                          _dateOfBirthController.text = formattedDate;
                        });
                      }
                    },
                  ),
                ]),
              )
            ],
          ),
          const SizedBox(height: 60.0),
          GestureDetector(
            onTap: (() {
              //main button register
              register();
            }),
            child: Container(
              width: double.infinity,
              height: 50,
              child: Text("Register",
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
          Align(
            alignment: Alignment.centerRight,
            child: GestureDetector(
              onTap: () {
                Navigator.pushNamed(context, Login.routeName);
              }, //redirect to login
              child: Text(
                "Already registered?",
                style: TextStyle(fontSize: 12, fontWeight: FontWeight.w400),
              ),
            ),
          ),
          const SizedBox(height: 30.0),
          Padding(
            padding: const EdgeInsets.all(15),
            child: Text(
              "By registering you agree to our terms, conditions and privacy policy.",
              textAlign: TextAlign.justify,
            ),
          ),
          const SizedBox(
            height: 10,
          ),
        ],
      ),
    ));
  }

  double screenWidthPercentage(BuildContext context, {double percentage = 1}) =>
      MediaQuery.of(context).size.width * percentage;

  register() async {
    try {
      var request = UserInsertRequest();

      request.firstName = _firstNameController.text;
      request.lastName = _lastNameController.text;
      request.email = _emailController.text;
      request.phoneNumber = _phoneNumberController.text;
      request.gender = _genderController.text;
      request.username = _usernameController.text;
      request.dateOfBirth = _dateOfBirthController.text;
      request.password = _passwordController.text;
      request.passwordConfirmation = _passwordConfirmationController.text;
      request.roleId = 0;

      var response = await _userProvider.register(request);

      showDialog(
          context: context,
          builder: (BuildContext context) => AlertDialog(
                title: Text("Success"),
                content: Text("Congrats! Now log in with your credentials."),
                actions: [
                  TextButton(
                      onPressed: () async =>
                          await Navigator.pushNamed(context, Login.routeName),
                      child: Text("Ok"))
                ],
              ));
    } on Exception catch (e) {
      showDialog(
          context: context,
          builder: (BuildContext context) => AlertDialog(
                title: Text("Error"),
                content: Text(e.toString()),
                actions: [
                  TextButton(
                      onPressed: () => Navigator.pop(context),
                      child: Text("Ok"))
                ],
              ));
    }
  }
}
