import 'dart:convert';
import 'package:http/http.dart' as http;
import 'package:flutter/material.dart';
import 'package:flutter/src/foundation/key.dart';
import 'package:flutter/src/widgets/framework.dart';
import 'package:flutter_stripe/flutter_stripe.dart';
import 'package:intl/intl.dart';
import 'package:provider/provider.dart';
import 'package:the_lions_den_mobile/model/facility/facility_response.dart';
import 'package:the_lions_den_mobile/model/reservation/payment_details_insert_request.dart';
import 'package:the_lions_den_mobile/model/reservation/reservation_insert_request.dart';
import 'package:the_lions_den_mobile/model/room/room_response.dart';
import 'package:the_lions_den_mobile/pages/user/user_profile.dart';
import 'package:the_lions_den_mobile/providers/facility_provider.dart';
import 'package:the_lions_den_mobile/providers/reservation_provider.dart';
import 'package:the_lions_den_mobile/providers/room_provider.dart';
import 'package:the_lions_den_mobile/utils/auth_helper.dart';
import 'package:the_lions_den_mobile/utils/number_formatter.dart';
import 'package:the_lions_den_mobile/utils/util.dart';
import 'package:the_lions_den_mobile/widgets/tld_appbar.dart';
import 'package:the_lions_den_mobile/widgets/tld_drawer.dart';

class RoomReservation extends StatefulWidget {
  static const String routeName = "/roomReservation";
  String id;
  RoomReservation({Key? key, required this.id}) : super(key: key);

  @override
  State<RoomReservation> createState() => _RoomReservationState(id);
}

class _RoomReservationState extends State<RoomReservation> {
  String id;
  RoomProvider? _roomProvider;
  RoomResponse data = RoomResponse();
  FacilityProvider? _facilityProvider;
  List<FacilityResponse> facilities = <FacilityResponse>[];
  List<FacilityResponse> selectedFacilites = <FacilityResponse>[];
  List<FacilityResponse> recommendedFacilities = <FacilityResponse>[];
  ReservationProvider? _reservationProvider;

  final TextEditingController _firstNameController = TextEditingController();
  final TextEditingController _lastNameController = TextEditingController();
  final TextEditingController _emailController = TextEditingController();
  final TextEditingController _phoneNumberController = TextEditingController();
  final TextEditingController _arrivalController = TextEditingController();
  final TextEditingController _departureController = TextEditingController();

  final _formKey = GlobalKey<FormState>();

  var bookedDates = [];

  var arrivalTimes = [
    "08:00am - 09:00am",
    "09:00am - 10:00am",
    "10:00am - 11:00am",
    "11:00am - 12:00am"
  ];
  int selectedArrivalTime = 99;
  int selectedFacility = 99;

  DateFormat dt = DateFormat('EEE, M/d/y');
  _RoomReservationState(this.id);

  //stripe

  Map<String, dynamic>? paymentIntentData;

  DateTimeRange dateTimeRange = DateTimeRange(
      start: DateTime.now(), end: DateTime.now().add(const Duration(days: 1)));

  bool isAvaliable = false;

  @override
  void initState() {
    super.initState();
    _roomProvider = context.read<RoomProvider>();
    _facilityProvider = context.read<FacilityProvider>();
    _reservationProvider = context.read<ReservationProvider>();
    Stripe.publishableKey =
        "pk_test_51KR05DIwNGlyHmAKnz8PDxOojJ1pXkmGAek19LTK4XY8oR6XAkpQgoZHE0ESAAdMjuFsT2QFV1NXSFAaW1XrLTdb00G3xxz2CI";
    fillTextFields();
    loadData();
  }

  Future loadData() async {
    var tempData = await _roomProvider?.getById(int.parse(id));
    var tempFac = await _facilityProvider?.get({"status": "Active"});
    var bookedDateTmp = await _roomProvider?.getBookedDates(int.parse(id));
    setState(() {
      data = tempData!;
      facilities = tempFac!;
      bookedDates = bookedDateTmp!;
    });
  }

  Future loadReccommendedFacilities(int idToRec) async {
    recommendedFacilities = [];
    var tempRecFac = await _facilityProvider!.reccomend(idToRec);
    setState(() {
      recommendedFacilities = tempRecFac;
    });
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
        drawer: TLDDrawer(),
        appBar: TLDAppbar(title: "ROOM RESERVATION", appBar: AppBar()),
        body: SafeArea(
            child: SingleChildScrollView(
          child: Column(
              crossAxisAlignment: CrossAxisAlignment.center,
              children: _buildAll()),
        )));
  }

  List<Widget> _buildAll() {
    if (data.name == null) {
      return [
        const SizedBox(
          height: 50,
        ),
        const Center(child: Text("Loading room."))
      ];
    }

    List<Widget> list = <Widget>[];
    list.add(_buildHeader());
    list.add(_buildReservationForm());
    list.add(_buildFacilitySection());
    list.add(_buildTotalPriceSection());
    list.addAll(_buildCompleteReservation());
    return list;
  }

  Widget _buildHeader() {
    return Container(
      padding: const EdgeInsets.symmetric(horizontal: 20, vertical: 10),
      child: Column(
        mainAxisAlignment: MainAxisAlignment.start,
        crossAxisAlignment: CrossAxisAlignment.center,
        children: [
          Row(
            mainAxisAlignment: MainAxisAlignment.spaceBetween,
            children: [
              Text(
                data.name!,
                style: TextStyle(
                    color: Color.fromARGB(255, 66, 129, 160),
                    fontSize: 24,
                    fontWeight: FontWeight.bold),
              ),
              Text(
                "${data.price!}\$ /per night",
                textAlign: TextAlign.left,
                style:
                    const TextStyle(fontSize: 14, fontWeight: FontWeight.bold),
              ),
            ],
          ),
          SizedBox(
            height: 10,
          ),
          Text(
            data.roomTypeName!,
            textAlign: TextAlign.center,
            style: const TextStyle(fontSize: 14, fontWeight: FontWeight.w400),
          ),
        ],
      ),
    );
  }

  Widget _buildReservationForm() {
    final start = dateTimeRange.start;
    final end = dateTimeRange.end;

    return Column(
      children: [
        Padding(
          padding: EdgeInsets.all(30),
          child: Form(
              key: _formKey,
              child: Column(children: [
                TextFormField(
                  keyboardType: TextInputType.none,
                  controller: _arrivalController,
                  decoration: InputDecoration(
                      labelText: "Arrival", icon: Icon(Icons.calendar_today)),
                  onTap: () async {
                    DateTimeRange? pickedDate = await showDateRangePicker(
                        context: context,
                        initialDateRange: dateTimeRange,
                        firstDate: DateTime.now(),
                        lastDate: DateTime(2100));

                    if (pickedDate != null) {
                      dateTimeRange = pickedDate;
                      String pickedArrival =
                          DateFormat('yyyy-MM-dd').format(pickedDate.start);
                      String pickedDeparture =
                          DateFormat('yyyy-MM-dd').format(pickedDate.end);

                      isAvaliable = await _roomProvider!.checkAvailabilty(
                          data.roomId!, pickedDate.start, pickedDate.end);

                      setState(() {
                        _departureController.text = pickedDeparture;
                        _arrivalController.text = pickedArrival;
                      });
                    }
                  },
                ),
                TextFormField(
                  keyboardType: TextInputType.none,
                  controller: _departureController,
                  decoration: InputDecoration(
                      labelText: "Departure", icon: Icon(Icons.calendar_today)),
                  onTap: () async {
                    DateTimeRange? pickedDate = await showDateRangePicker(
                        context: context,
                        initialDateRange: dateTimeRange,
                        firstDate: DateTime(1950),
                        lastDate: DateTime(2100));

                    if (pickedDate != null) {
                      String pickedArrival =
                          DateFormat('yyyy-MM-dd').format(pickedDate.start);
                      String pickedDeparture =
                          DateFormat('yyyy-MM-dd').format(pickedDate.end);

                      setState(() {
                        _departureController.text = pickedDeparture;
                        _arrivalController.text = pickedArrival;
                      });
                    }
                  },
                ),
                const SizedBox(height: 15.0),
                Container(
                  height: 30,
                  width: 150,
                  decoration: BoxDecoration(
                      borderRadius: BorderRadius.circular(10),
                      gradient: const LinearGradient(colors: [
                        Color.fromARGB(255, 0, 69, 189),
                        Color.fromARGB(255, 10, 158, 227)
                      ])),
                  child: InkWell(
                    child: const Center(child: Text("Booked dates")),
                    onTap: () {
                      _showBookedDates();
                    },
                  ),
                ),
                const SizedBox(height: 15.0),
                DropdownButton(
                    items: _buildEstimatedArrivalTimeDownList(),
                    value: selectedArrivalTime,
                    icon: Icon(Icons.share_arrival_time),
                    hint: Text("Estimated arrival time"),
                    //hint: Text("Room Type"),
                    onChanged: (dynamic value) {
                      setState(() {
                        selectedArrivalTime = value;
                      });
                    }),
                const SizedBox(height: 35.0),
                Text(
                  "Personal details",
                  textAlign: TextAlign.center,
                  style: const TextStyle(
                      fontSize: 20, fontWeight: FontWeight.w900),
                ),
                SizedBox(
                  height: 20,
                ),
                TextFormField(
                  controller: _firstNameController,
                  decoration: InputDecoration(
                      labelText: "First Name", icon: Icon(Icons.person)),
                  enabled: false,
                ),
                const SizedBox(height: 5.0),
                TextFormField(
                  decoration: InputDecoration(
                      labelText: "Last Name", icon: Icon(Icons.person)),
                  controller: _lastNameController,
                  enabled: false,
                ),
                const SizedBox(height: 5.0),
                TextFormField(
                  decoration: InputDecoration(
                      labelText: "Email", icon: Icon(Icons.mail)),
                  controller: _emailController,
                  enabled: false,
                ),
                const SizedBox(height: 5.0),
                TextFormField(
                  decoration: InputDecoration(
                      labelText: "Phone Number", icon: Icon(Icons.phone)),
                  enabled: false,
                  controller: _phoneNumberController,
                ),
                const SizedBox(height: 5.0),
              ])),
        )
      ],
    );
  }

  String? requiredFieldValidation(String? value) {
    if (value == null || value.isEmpty) {
      return 'This field is required!';
    }
    return null;
  }

  void fillTextFields() {
    _firstNameController.text = AuthHelper.user!.firstName!;
    _lastNameController.text = AuthHelper.user!.lastName!;
    _emailController.text = AuthHelper.user!.email!;
    _phoneNumberController.text = AuthHelper.user!.phoneNumber!;
  }

  List<DropdownMenuItem> _buildEstimatedArrivalTimeDownList() {
    List<DropdownMenuItem> list = <DropdownMenuItem>[];

    list.add(DropdownMenuItem(
      child:
          Text("Estimated Arrival Time", style: TextStyle(color: Colors.black)),
      value: 99,
      enabled: false,
    ));

    list.addAll(arrivalTimes
        .map((x) => DropdownMenuItem(
              child: Text(x, style: TextStyle(color: Colors.black)),
              value: arrivalTimes.indexOf(x),
            ))
        .toList());

    return list;
  }

  Widget _buildFacilitySection() {
    return Column(
      children: [
        Padding(
          padding: const EdgeInsets.all(30),
          child: Column(
            children: [
              Text(
                "Include Facilites",
                textAlign: TextAlign.center,
                style:
                    const TextStyle(fontSize: 20, fontWeight: FontWeight.w900),
              ),
              const SizedBox(height: 35.0),
              DropdownButton(
                  items: _buildFacilitesDownList(),
                  value: selectedFacility,
                  icon: Icon(Icons.pool),
                  hint: Text("Facilities"),
                  onChanged: (dynamic value) {
                    setState(() {
                      recommendedFacilities = [];
                      selectedFacility = value;
                      loadReccommendedFacilities(value);
                    });
                  }),
              const SizedBox(
                height: 10,
              ),
              Row(
                mainAxisAlignment: MainAxisAlignment.spaceEvenly,
                children: [
                  GestureDetector(
                    onTap: (() {
                      showFacilityDetails();
                    }),
                    child: Container(
                      height: 30,
                      width: 150,
                      child: Text("Preview",
                          style: TextStyle(
                              fontSize: 16, fontWeight: FontWeight.bold)),
                      alignment: Alignment.center,
                      decoration: BoxDecoration(
                        borderRadius: BorderRadius.circular(10),
                        color: Colors.grey,
                      ),
                    ),
                  ),
                  GestureDetector(
                    onTap: (() {
                      setState(() {
                        selectedFacilites.add(facilities.firstWhere((element) =>
                            element.facilityId == selectedFacility));
                        facilities.removeWhere((element) =>
                            element.facilityId == selectedFacility);
                        selectedFacility = 99;
                      });
                    }),
                    child: Container(
                      width: 150,
                      height: 30,
                      child: Text("Include",
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
                ],
              ),
              const SizedBox(
                height: 10,
              ),
              recommendedFacilities.isNotEmpty
                  ? Text(
                      "Frequently reserved together:",
                      textAlign: TextAlign.center,
                      style: const TextStyle(
                          fontSize: 14, fontWeight: FontWeight.w400),
                    )
                  : SizedBox(),
              const SizedBox(
                height: 15,
              ),
              selectedFacility != 99
                  ? _buildRecommendedFacilities()
                  : const SizedBox(),
              const SizedBox(
                height: 15,
              ),
            ],
          ),
        ),
      ],
    );
  }

  List<DropdownMenuItem> _buildFacilitesDownList() {
    List<DropdownMenuItem> list = <DropdownMenuItem>[];

    list.add(DropdownMenuItem(
      child: Text("Select a facility you want to include",
          style: TextStyle(color: Colors.black)),
      value: 99,
      enabled: false,
    ));

    list.addAll(facilities
        .map((x) => DropdownMenuItem(
              value: x.facilityId,
              child: Text("${x.name} - ${x.price}\$",
                  style: TextStyle(color: Colors.black)),
            ))
        .toList());

    return list;
  }

  void showFacilityDetails() {
    if (selectedFacility != 99) {
      var x = facilities
          .firstWhere((element) => element.facilityId == selectedFacility);
      showDialog(
          context: context,
          builder: (BuildContext context) => AlertDialog(
                title: Text(x.name!),
                content: Container(
                  height: 250,
                  width: 250,
                  child: Column(
                    children: [
                      Container(
                        height: 200,
                        width: 200,
                        child: imageFromBase64String(x.image!),
                      ),
                      Text(x.description ?? ""),
                      Text(formatNumber(x.price) + "\$"),
                    ],
                  ),
                ),
                actions: [
                  TextButton(
                      onPressed: () => Navigator.pop(context),
                      child: Text("Ok"))
                ],
              ));
    } else {
      showDialog(
          context: context,
          builder: (BuildContext context) => AlertDialog(
                title: Text("Info"),
                content: Text("Select a facility first."),
                actions: [
                  TextButton(
                      onPressed: () => Navigator.pop(context),
                      child: Text("Ok"))
                ],
              ));
    }
  }

  Widget _buildRecommendedFacilities() {
    if (recommendedFacilities.isEmpty) {
      return const CircularProgressIndicator(
        color: Colors.black,
      );
    }

    List<Widget> list = recommendedFacilities
        .map((x) => Container(
              child: Column(
                children: [
                  Container(
                    height: 130,
                    width: 100,
                    child: imageFromBase64String(x.image!),
                  ),
                  Text(x.name ?? ""),
                  Text(x.description ?? ""),
                  Text(formatNumber(x.price)),
                ],
              ),
            ))
        .cast<Widget>()
        .toList();

    return Container(
        height: 200,
        child: GridView(
          gridDelegate: SliverGridDelegateWithFixedCrossAxisCount(
              crossAxisCount: 1,
              childAspectRatio: 1,
              crossAxisSpacing: 20,
              mainAxisSpacing: 30),
          scrollDirection: Axis.horizontal,
          children: list,
        ));
  }

  Widget _buildTotalPriceSection() {
    if (_arrivalController.text.isEmpty || _departureController.text.isEmpty) {
      return const Center(
        child: Text("Please select your arrival and deparutre dates."),
      );
    }

    var arrivalDate = DateTime.parse(_arrivalController.text);
    var departureDate = DateTime.parse(_departureController.text);

    var roomPrice = departureDate.difference(arrivalDate).inDays * data.price!;

    return Column(
      children: [
        Padding(
          padding: const EdgeInsets.all(30),
          child: Column(children: [
            Text(
              "Total price",
              textAlign: TextAlign.center,
              style: const TextStyle(fontSize: 22, fontWeight: FontWeight.w900),
            ),
            SizedBox(
              height: 20,
            ),
            Text(
              "${dt.format(arrivalDate)} - ${dt.format(departureDate)}",
              textAlign: TextAlign.center,
              style: const TextStyle(fontSize: 16, fontWeight: FontWeight.w400),
            ),
            const SizedBox(height: 35.0),
            Row(
              mainAxisAlignment: MainAxisAlignment.spaceBetween,
              children: [
                Text(
                  "Room: ${data.name} (${departureDate.difference(arrivalDate).inDays} days)",
                  textAlign: TextAlign.center,
                  style: const TextStyle(
                      fontSize: 16, fontWeight: FontWeight.w400),
                ),
                Text(
                  "$roomPrice\$",
                  textAlign: TextAlign.center,
                  style: const TextStyle(
                      fontSize: 16, fontWeight: FontWeight.w400),
                ),
              ],
            ),
            const SizedBox(
              height: 10,
            ),
            Column(
              children: _buildAddedFacilitesList(
                  departureDate.difference(arrivalDate).inDays),
            ),
            const SizedBox(
              height: 15,
            ),
            Row(
              mainAxisAlignment: MainAxisAlignment.spaceBetween,
              children: [
                Text(
                  "Total:",
                  textAlign: TextAlign.center,
                  style: const TextStyle(
                      fontSize: 16, fontWeight: FontWeight.w400),
                ),
                Text(
                  "${_calculateTotalPrice()}\$",
                  textAlign: TextAlign.center,
                  style: const TextStyle(
                      fontSize: 16, fontWeight: FontWeight.w400),
                ),
              ],
            ),
          ]),
        ),
      ],
    );
  }

  _buildAddedFacilitesList(int days) {
    if (selectedFacilites.isEmpty) return [const SizedBox()];

    List<Widget> list = selectedFacilites
        .map((e) => Row(
              mainAxisAlignment: MainAxisAlignment.spaceBetween,
              children: [
                Column(
                  children: [
                    Row(
                      children: [
                        GestureDetector(
                          child: Icon(
                            Icons.remove_circle_outline,
                            size: 18,
                          ),
                          onTap: () {
                            setState(() {
                              facilities.add(selectedFacilites.firstWhere(
                                  (element) =>
                                      element.facilityId == e.facilityId));
                              selectedFacilites.removeWhere((element) =>
                                  element.facilityId == e.facilityId);
                            });
                          },
                        ),
                        Text(
                          "${e.name}",
                          textAlign: TextAlign.center,
                          style: const TextStyle(
                              fontSize: 16, fontWeight: FontWeight.w400),
                        ),
                      ],
                    ),
                  ],
                ),
                Text(
                  "${e.price! * days}\$",
                  textAlign: TextAlign.center,
                  style: const TextStyle(
                      fontSize: 16, fontWeight: FontWeight.w400),
                ),
              ],
            ))
        .cast<Widget>()
        .toList();

    return list;
  }

  _calculateTotalPrice() {
    double totalPrice = 0;
    var arrivalDate = DateTime.parse(_arrivalController.text);
    var departureDate = DateTime.parse(_departureController.text);
    var days = departureDate.difference(arrivalDate).inDays;

    totalPrice += days * data.price!;

    selectedFacilites.forEach((element) {
      totalPrice += element.price! * days;
    });

    return totalPrice;
  }

  List<Widget> _buildCompleteReservation() {
    if (isAvaliable) {
      return [
        SizedBox(
          height: 20,
        ),
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
            child: const Center(child: Text("Complete reservation")),
            onTap: () async {
              makePayment(_calculateTotalPrice());
            },
          ),
        ),
        SizedBox(
          height: 20,
        )
      ];
    } else {
      return [
        SizedBox(
          height: 20,
        ),
        Container(
          height: 50,
          width: 350,
          decoration: BoxDecoration(
              borderRadius: BorderRadius.circular(10),
              gradient: const LinearGradient(colors: [
                Color.fromARGB(255, 189, 0, 0),
                Color.fromARGB(255, 126, 4, 4)
              ])),
          child: InkWell(
            child: const Center(
                child: Text(
                    "Room is not avaliable for the selected time period.")),
            onTap: () async {},
          ),
        ),
        SizedBox(
          height: 20,
        )
      ];
    }
  }

  //STRIPE

  Future<void> makePayment(double amount) async {
    try {
      paymentIntentData =
          await createPaymentIntent((amount * 100).round().toString(), 'usd');
      var response = await Stripe.instance
          .initPaymentSheet(
              paymentSheetParameters: SetupPaymentSheetParameters(
                  paymentIntentClientSecret:
                      paymentIntentData!['client_secret'],
                  applePay: true,
                  googlePay: true,
                  testEnv: true,
                  style: ThemeMode.light,
                  merchantCountryCode: 'Curacao',
                  merchantDisplayName: 'The Lion\' Den'))
          .then((value) {});

      displayPaymentSheet();
    } on Exception catch (e) {
      showDialog(
          context: context,
          builder: (BuildContext context) => AlertDialog(
                title: const Text("Error ocurred"),
                content: Text(e.toString()),
                actions: [
                  TextButton(
                    child: const Text("Ok"),
                    onPressed: () => Navigator.pop(context),
                  )
                ],
              ));
    }
  }

  displayPaymentSheet() async {
    try {
      await Stripe.instance
          .presentPaymentSheet(
              parameters: PresentPaymentSheetParameters(
        clientSecret: paymentIntentData!['client_secret'],
        confirmPayment: true,
      ))
          .onError((error, stackTrace) {
        throw Exception(error);
      });

      createReservationPayment();
      ScaffoldMessenger.of(context)
          .showSnackBar(const SnackBar(content: Text("Payment successful")));
    } on Exception catch (e) {
      showDialog(
          context: context,
          builder: (_) => AlertDialog(
                title: Text("Error ocurred"),
                content: Text("Payment canceled!"),
              ));
    } catch (e) {
      print('$e');
    }
  }

  createPaymentIntent(String amount, String currency) async {
    try {
      Map<String, dynamic> body = {
        'amount': amount,
        'currency': currency,
        'payment_method_types[]': 'card'
      };

      var response = await http.post(
          Uri.parse('https://api.stripe.com/v1/payment_intents'),
          body: body,
          headers: {
            'Authorization':
                'Bearer sk_test_51KR05DIwNGlyHmAKv1n1TGR3LZ5bgvIsSEozrU8rWs8usz8BtEHuhsYwPIlVnDNsZL8rcJV6m65oMudBj4eSQSBE00yEuWupGX',
            'Content-Type': 'application/x-www-form-urlencoded'
          });
      return jsonDecode(response.body);
    } catch (err) {
      print('Error: ${err.toString()}');
    }
  }

  void createReservationPayment() async {
    var request = _prepareInsertRequest();

    var response = await _reservationProvider!.insert(request);

    showDialog(
        context: context,
        builder: (BuildContext context) => AlertDialog(
              title: const Text("Success"),
              content: const Text("Room reserved successfully."),
              actions: [
                TextButton(
                    onPressed: () async => await Navigator.popAndPushNamed(
                        context, UserProfile.routeName),
                    child: const Text("Ok"))
              ],
            ));
  }

  _prepareInsertRequest() {
    var req = ReservationInsertRequest();
    req.arrival = DateTime.parse(_arrivalController.text);
    req.departure = DateTime.parse(_departureController.text);
    req.totalPrice = _calculateTotalPrice();
    req.estimatedArrivalTime =
        selectedArrivalTime == 99 ? null : arrivalTimes[selectedArrivalTime];
    req.roomId = data.roomId;
    req.userId = AuthHelper.user!.userId!;
    var paymentDetails = PaymetDetailsInsertRequest();
    paymentDetails.currency = paymentIntentData!['currency'];
    paymentDetails.stripeId = paymentIntentData!['id'];
    paymentDetails.date = DateTime.now();
    paymentDetails.paymentType = "Stripe payment";
    req.paymentDetails = paymentDetails;
    req.facilityIds =
        selectedFacilites.map((e) => e.facilityId).cast<int>().toList();
    return req;
  }

  void _showBookedDates() {
    if (bookedDates.isEmpty) {
      showDialog(
          context: context,
          builder: (BuildContext context) => AlertDialog(
                title: Text("Booked dates"),
                content: Container(
                    width: 100,
                    height: 100,
                    child: Center(child: Text("Not future reservations."))),
                actions: [
                  TextButton(
                      onPressed: () => Navigator.pop(context),
                      child: Text("Ok"))
                ],
              ));
    } else {
      showDialog(
          context: context,
          builder: (BuildContext context) => AlertDialog(
                title: Text("Booked dates"),
                content: Container(
                    width: 100,
                    height: 100,
                    child: GridView(
                        gridDelegate: SliverGridDelegateWithFixedCrossAxisCount(
                            crossAxisCount: 1,
                            childAspectRatio: 4 / 3,
                            crossAxisSpacing: 10,
                            mainAxisSpacing: 20),
                        scrollDirection: Axis.vertical,
                        children: bookedDates
                            .map((e) => Container(
                                  child: Text(e),
                                  height: 20,
                                ))
                            .cast<Widget>()
                            .toList())),
                actions: [
                  TextButton(
                      onPressed: () => Navigator.pop(context),
                      child: Text("Ok"))
                ],
              ));
    }
  }
}
