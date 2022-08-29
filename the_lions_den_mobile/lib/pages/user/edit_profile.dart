import 'package:flutter/material.dart';
import 'package:flutter/src/foundation/key.dart';
import 'package:flutter/src/widgets/framework.dart';
import 'package:intl/intl.dart';
import 'package:provider/provider.dart';
import 'package:the_lions_den_mobile/model/user/user_insert_request.dart';
import 'package:the_lions_den_mobile/model/user/user_response.dart';
import 'package:the_lions_den_mobile/pages/user/user_profile.dart';
import 'package:the_lions_den_mobile/providers/user_provider.dart';
import 'package:the_lions_den_mobile/utils/auth_helper.dart';
import 'package:the_lions_den_mobile/widgets/tld_appbar.dart';
import 'package:the_lions_den_mobile/widgets/tld_drawer.dart';

class EditProfile extends StatefulWidget {
  static const routeName = "/edit-profile";
  const EditProfile({Key? key}) : super(key: key);

  @override
  State<EditProfile> createState() => _EditProfileState();
}

class _EditProfileState extends State<EditProfile> {
  final TextEditingController _firstNameController = TextEditingController();
  final TextEditingController _lastNameController = TextEditingController();
  final TextEditingController _passwordController = TextEditingController();
  final TextEditingController _passwordConfirmationController =
      TextEditingController();
  final TextEditingController _emailController = TextEditingController();
  final TextEditingController _phoneNumberController = TextEditingController();
  final TextEditingController _genderController = TextEditingController();
  final TextEditingController _dateOfBirthController = TextEditingController();

  UserProvider? _userProvider;
  UserResponse data = UserResponse();

  final _formKey = GlobalKey<FormState>();

  @override
  void initState() {
    super.initState();
    _userProvider = context.read<UserProvider>();
    loadData();
  }

  Future loadData() async {
    var tempData = await _userProvider?.getById(AuthHelper.user!.userId!);
    setState(() {
      data = tempData!;
      _firstNameController.text = tempData.firstName!;
      _lastNameController.text = tempData.lastName!;
      _emailController.text = tempData.email!;
      _phoneNumberController.text = tempData.phoneNumber!;
      _genderController.text = tempData.gender!;
      _dateOfBirthController.text =
          DateFormat('yyyy-MM-dd').format(tempData.dateOfBirth!);
    });
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: _buildEditProfile(),
      drawer: TLDDrawer(),
      appBar: TLDAppbar(title: "EDIT PROFILE", appBar: AppBar()),
    );
  }

  String? requiredFieldValidation(String? value) {
    if (value == null || value.isEmpty) {
      return 'This field is required!';
    }
    return null;
  }

  _buildEditProfile() {
    if (data.username == null) {
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
            height: 125,
            decoration: const BoxDecoration(
                image: DecorationImage(
                    image: AssetImage("assets/images/logo.png"),
                    fit: BoxFit.fill)),
          ),
          const SizedBox(height: 10.0),
          const SizedBox(height: 40.0),
          Column(
            children: [
              Padding(
                padding: EdgeInsets.all(10),
                child: Form(
                    key: _formKey,
                    child: Column(children: [
                      TextFormField(
                        controller: _firstNameController,
                        decoration: InputDecoration(
                            labelText: "First Name", icon: Icon(Icons.person)),
                        validator: (String? value) {
                          return requiredFieldValidation(value);
                        },
                      ),
                      const SizedBox(height: 5.0),
                      TextFormField(
                        decoration: InputDecoration(
                            labelText: "Last Name", icon: Icon(Icons.person)),
                        controller: _lastNameController,
                        validator: (String? value) {
                          return requiredFieldValidation(value);
                        },
                      ),
                      const SizedBox(height: 5.0),
                      TextFormField(
                        decoration: InputDecoration(
                            labelText: "Email", icon: Icon(Icons.mail)),
                        controller: _emailController,
                        validator: (String? value) {
                          return requiredFieldValidation(value);
                        },
                      ),
                      const SizedBox(height: 5.0),
                      TextFormField(
                        decoration: InputDecoration(
                            labelText: "Phone Number", icon: Icon(Icons.phone)),
                        controller: _phoneNumberController,
                      ),
                      const SizedBox(height: 5.0),
                      TextFormField(
                          decoration: InputDecoration(
                              labelText: "Password",
                              icon: Icon(Icons.password)),
                          obscureText: true,
                          controller: _passwordController),
                      const SizedBox(height: 5.0),
                      TextFormField(
                          decoration: InputDecoration(
                              labelText: "Password Confirmation",
                              icon: Icon(Icons.password)),
                          obscureText: true,
                          controller: _passwordConfirmationController),
                      TextFormField(
                        decoration: InputDecoration(
                            labelText: "Gender",
                            icon: Icon(Icons.female_sharp)),
                        controller: _genderController,
                      ),
                      TextFormField(
                        controller: _dateOfBirthController,
                        decoration: InputDecoration(
                            labelText: "Date of birth",
                            icon: Icon(Icons.calendar_today)),
                        onTap: () async {
                          DateTime? pickedDate = await showDatePicker(
                              context: context,
                              initialDate: DateTime.now(),
                              firstDate: DateTime(1900),
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
                    ])),
              )
            ],
          ),
          const SizedBox(height: 60.0),
          GestureDetector(
            onTap: (() {
              if (_formKey.currentState!.validate()) {
                saveChanges();
              }
            }),
            child: Container(
              width: double.infinity,
              height: 50,
              child: Text("Save changes",
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
        ],
      ),
    );
  }

  saveChanges() async {
    try {
      var request = UserInsertRequest();

      request.firstName = _firstNameController.text;
      request.lastName = _lastNameController.text;
      request.email = _emailController.text;
      request.phoneNumber = _phoneNumberController.text.isEmpty
          ? null
          : _phoneNumberController.text;
      request.gender =
          _genderController.text.isEmpty ? null : _genderController.text;
      request.username = null;
      request.dateOfBirth = _dateOfBirthController.text.isEmpty
          ? null
          : _dateOfBirthController.text;
      request.password = _passwordController.text;
      request.passwordConfirmation = _passwordConfirmationController.text;
      request.roleId = 0;

      var response = await _userProvider!
          .customerUpdate(AuthHelper.user!.userId!, request);

      AuthHelper.password = _passwordController.text;
      showDialog(
          context: context,
          builder: (BuildContext context) => AlertDialog(
                title: const Text("Success"),
                content: const Text("Changes saved successfully."),
                actions: [
                  TextButton(
                      onPressed: () async => await Navigator.pushNamed(
                          context, UserProfile.routeName),
                      child: const Text("Ok"))
                ],
              ));
    } on Exception catch (e) {
      showDialog(
          context: context,
          builder: (BuildContext context) => AlertDialog(
                title: const Text("Error"),
                content: Text(e.toString()),
                actions: [
                  TextButton(
                      onPressed: () => Navigator.pop(context),
                      child: const Text("Ok"))
                ],
              ));
    }
  }
}
