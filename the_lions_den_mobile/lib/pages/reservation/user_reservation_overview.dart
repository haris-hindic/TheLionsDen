import 'package:flutter/material.dart';
import 'package:flutter/src/foundation/key.dart';
import 'package:flutter/src/widgets/framework.dart';
import 'package:intl/intl.dart';
import 'package:provider/provider.dart';
import 'package:the_lions_den_mobile/model/reservation/reservation_response.dart';
import 'package:the_lions_den_mobile/providers/reservation_provider.dart';
import 'package:the_lions_den_mobile/utils/auth_helper.dart';
import 'package:the_lions_den_mobile/widgets/tld_appbar.dart';
import 'package:the_lions_den_mobile/widgets/tld_drawer.dart';

class UserReservationOverview extends StatefulWidget {
  static const String routeName = "/reservation-overview";
  const UserReservationOverview({Key? key}) : super(key: key);

  @override
  State<UserReservationOverview> createState() =>
      _UserReservationOverviewState();
}

class _UserReservationOverviewState extends State<UserReservationOverview> {
  ReservationProvider? _reservationProvider;
  List<ReservationResponse> data = [];
  var reservationStatus = ["Active", "Cancelled", "Confirmed"];
  int selectedReservationStatusIndex = 99;
  TextEditingController _reservationStatusController =
      new TextEditingController();
  DateFormat dt = DateFormat('EEE, M/d/y');

  @override
  void initState() {
    super.initState();
    _reservationProvider = context.read<ReservationProvider>();
    loadData();
  }

  Future loadData() async {
    var search = {
      "UserId": AuthHelper.user!.userId,
      "IncludeRoom": "true",
      "IncludeFacilites": "true"
    };
    var tempData = await _reservationProvider?.get(search);
    setState(() {
      data = tempData!;
    });
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
        drawer: TLDDrawer(),
        appBar: TLDAppbar(title: "RESERVATION OVERVIEW", appBar: AppBar()),
        body: SafeArea(
            child: SingleChildScrollView(
          child: Column(
              crossAxisAlignment: CrossAxisAlignment.start,
              children: _buildAll()),
        )));
  }

  List<Widget> _buildAll() {
    List<Widget> list = <Widget>[];
    list.add(_buildHeader());
    list.add(_buildReservationSearch());
    list.addAll(_buildReservationCardList());
    return list;
  }

  Widget _buildHeader() {
    return Container(
      padding: const EdgeInsets.symmetric(horizontal: 20, vertical: 10),
      child: const Text(
        "Your reservations",
        style: TextStyle(
            color: Color.fromARGB(255, 66, 129, 160),
            fontSize: 20,
            fontWeight: FontWeight.bold),
      ),
    );
  }

  Widget _buildReservationSearch() {
    return Column(
      children: [
        Row(
          mainAxisAlignment: MainAxisAlignment.spaceEvenly,
          children: [
            Expanded(
                child: Container(
              padding: EdgeInsets.symmetric(horizontal: 10, vertical: 5),
              child: DropdownButton(
                  items: _buildReservationStatusDownList(),
                  value: selectedReservationStatusIndex,
                  icon: Icon(Icons.add_task),
                  //hint: Text("Room Type"),
                  onChanged: (dynamic value) {
                    setState(() {
                      selectedReservationStatusIndex = value;
                    });
                  }),
            )),
          ],
        ),
        Row(
          mainAxisAlignment: MainAxisAlignment.spaceBetween,
          children: [
            Expanded(
              child: Container(
                padding: EdgeInsets.symmetric(horizontal: 5, vertical: 5),
                child: IconButton(
                  icon: Icon(Icons.delete),
                  onPressed: () async {
                    setState(() {
                      selectedReservationStatusIndex = 99;
                    });
                  },
                ),
              ),
            ),
            Expanded(
              child: Container(
                padding: EdgeInsets.symmetric(horizontal: 5, vertical: 5),
                child: IconButton(
                  icon: Icon(Icons.filter_list_alt),
                  onPressed: () async {
                    var search = {
                      "UserId": AuthHelper.user!.userId,
                      "IncludeRoom": "true",
                      "IncludeFacilites": "true",
                      "Status": selectedReservationStatusIndex != 99
                          ? reservationStatus[selectedReservationStatusIndex]
                          : ''
                    };
                    var tmpData = await _reservationProvider?.get(search);
                    setState(() {
                      data = tmpData!;
                    });
                  },
                ),
              ),
            ),
          ],
        ),
      ],
    );
  }

  List<DropdownMenuItem> _buildReservationStatusDownList() {
    List<DropdownMenuItem> list = <DropdownMenuItem>[];

    list.add(DropdownMenuItem(
      child: Text("Status", style: TextStyle(color: Colors.black)),
      value: 99,
      enabled: false,
    ));

    list.addAll(reservationStatus
        .map((x) => DropdownMenuItem(
              child: Text(x, style: TextStyle(color: Colors.black)),
              value: reservationStatus.indexOf(x),
            ))
        .toList());

    return list;
  }

  List<Widget> _buildReservationCardList() {
    if (data.length == 0) {
      return [Center(child: const Text("Loading....."))];
    }

    List<Widget> list = data
        .map((x) => Container(
              //height: 225,
              child: Padding(
                padding: const EdgeInsets.all(16.0),
                child: Card(
                  color: const Color.fromARGB(255, 98, 161, 193),
                  child: ClipRRect(
                    child: Stack(children: [
                      Column(
                        children: [
                          Row(
                            mainAxisAlignment: MainAxisAlignment.center,
                            crossAxisAlignment: CrossAxisAlignment.start,
                            children: [
                              Expanded(
                                  child: Container(
                                child: Padding(
                                    padding: const EdgeInsets.only(
                                        top: 8, bottom: 8, right: 8, left: 16),
                                    child: Column(
                                      mainAxisAlignment:
                                          MainAxisAlignment.center,
                                      crossAxisAlignment:
                                          CrossAxisAlignment.start,
                                      children: [
                                        Row(
                                          mainAxisAlignment:
                                              MainAxisAlignment.spaceBetween,
                                          crossAxisAlignment:
                                              CrossAxisAlignment.center,
                                          children: [
                                            GestureDetector(
                                              // onTap: () => Navigator.pushNamed(
                                              //     context,
                                              //     "${RoomDetails.routeName}/${x.roomId}"),
                                              child: Text(
                                                "${dt.format(x.arrival!)} - ${dt.format(x.departure!)}",
                                                textAlign: TextAlign.left,
                                                style: const TextStyle(
                                                    fontSize: 16,
                                                    fontWeight:
                                                        FontWeight.bold),
                                              ),
                                            ),
                                            Text(
                                              "${x.status}",
                                              textAlign: TextAlign.right,
                                              style: const TextStyle(
                                                  fontSize: 16,
                                                  fontWeight: FontWeight.bold),
                                            ),
                                          ],
                                        ),
                                        Row(
                                          mainAxisAlignment:
                                              MainAxisAlignment.start,
                                          crossAxisAlignment:
                                              CrossAxisAlignment.center,
                                          children: [
                                            Text(
                                              x.roomName!,
                                              textAlign: TextAlign.left,
                                              style: const TextStyle(
                                                  fontSize: 12,
                                                  fontWeight: FontWeight.w400),
                                            ),
                                          ],
                                        ),
                                        Row(
                                          mainAxisAlignment:
                                              MainAxisAlignment.spaceBetween,
                                          crossAxisAlignment:
                                              CrossAxisAlignment.center,
                                          children: [
                                            _isActive(x),
                                            Text(
                                              "${x.totalPrice!}\$",
                                              textAlign: TextAlign.left,
                                              style: const TextStyle(
                                                  fontSize: 14,
                                                  fontWeight: FontWeight.bold),
                                            ),
                                          ],
                                        ),
                                      ],
                                    )),
                              ))
                            ],
                          )
                        ],
                      )
                    ]),
                  ),
                ),
              ),
            ))
        .cast<Widget>()
        .toList();

    return list;
  }

  _isActive(ReservationResponse x) {
    if (x.status == 'Active') {
      return IconButton(
        icon: Icon(Icons.cancel_outlined),
        onPressed: () async {
          showDialog(
              context: context,
              builder: (BuildContext context) => AlertDialog(
                    title: Text("Warning"),
                    content: Text("Would you like to cancel this reservation?"),
                    actions: [
                      TextButton(
                        child: Text("Yes"),
                        onPressed: () async {
                          await _reservationProvider!.cancel(x.reservationId!);
                          loadData();
                          Navigator.pop(context);
                        },
                      ),
                      TextButton(
                        child: Text("No"),
                        onPressed: () {
                          Navigator.pop(context);
                        },
                      ),
                    ],
                  ));
        },
      );
    } else {
      return Spacer();
    }
  }
}
