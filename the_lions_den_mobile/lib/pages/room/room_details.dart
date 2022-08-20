import 'package:carousel_slider/carousel_slider.dart';
import 'package:flutter/material.dart';
import 'package:provider/provider.dart';
import 'package:the_lions_den_mobile/model/room/room_response.dart';
import 'package:the_lions_den_mobile/model/room_image/room_image_response.dart';
import 'package:the_lions_den_mobile/providers/room_provider.dart';
import 'package:the_lions_den_mobile/utils/auth_helper.dart';
import 'package:the_lions_den_mobile/utils/util.dart';
import 'package:the_lions_den_mobile/widgets/tld_appbar.dart';
import 'package:the_lions_den_mobile/widgets/tld_drawer.dart';

class RoomDetails extends StatefulWidget {
  static const String routeName = "/roomDetails";
  String id;

  RoomDetails(this.id, {Key? key}) : super(key: key);

  @override
  State<RoomDetails> createState() => _RoomDetailsState(this.id);
}

class _RoomDetailsState extends State<RoomDetails> {
  RoomProvider? _roomProvider;
  RoomResponse data = RoomResponse();
  List<RoomImageResponse> images = [];
  String id;

  _RoomDetailsState(this.id);

  @override
  void initState() {
    super.initState();
    _roomProvider = context.read<RoomProvider>();
    loadData();
  }

  Future loadData() async {
    var tempData = await _roomProvider?.getById(int.parse(id));
    setState(() {
      data = tempData!;
      images = tempData.roomType!.roomImages!;
    });
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: _buildDetails(),
      drawer: TLDDrawer(),
      appBar: TLDAppbar(title: "ROOM DETAILS", appBar: AppBar()),
    );
  }

  _buildDetails() {
    if (data.coverImage == null) {
      return const Text("Loading.....");
    }

    return SafeArea(
      child: Stack(
        children: <Widget>[
          CarouselSlider(
            options: CarouselOptions(
              height: 350,
              autoPlay: true,
              autoPlayInterval: Duration(seconds: 1),
              autoPlayAnimationDuration: Duration(milliseconds: 1500),
              autoPlayCurve: Curves.fastOutSlowIn,
              enableInfiniteScroll: true,
            ),
            items: images
                .map((e) => Container(
                    width: 500,
                    foregroundDecoration: BoxDecoration(
                        color: Colors.black26,
                        border: Border.all(color: Colors.grey, width: 1)),
                    child: Image.memory(
                      dataFromBase64String(e.image as String),
                      fit: BoxFit.fill,
                    )))
                .toList(),
          ),
          SingleChildScrollView(
            padding: const EdgeInsets.only(top: 16.0, bottom: 20.0),
            child: Column(
              crossAxisAlignment: CrossAxisAlignment.start,
              children: <Widget>[
                const SizedBox(height: 250),
                Padding(
                  padding: const EdgeInsets.symmetric(horizontal: 16.0),
                  child: Text(
                    data.name!,
                    style: TextStyle(
                        color: Colors.white,
                        fontSize: 28.0,
                        fontWeight: FontWeight.bold),
                  ),
                ),
                Row(
                  children: <Widget>[
                    const SizedBox(width: 16.0),
                    Container(
                      padding: const EdgeInsets.symmetric(
                        vertical: 8.0,
                        horizontal: 16.0,
                      ),
                      decoration: BoxDecoration(
                          color: Colors.grey,
                          borderRadius: BorderRadius.circular(20.0)),
                      child: Text(
                        data.roomTypeName!,
                        style: TextStyle(color: Colors.white, fontSize: 13.0),
                      ),
                    ),
                    Spacer(),
                    isSaved(data)
                  ],
                ),
                Container(
                  padding: const EdgeInsets.all(32.0),
                  color: Colors.white,
                  child: Column(
                    crossAxisAlignment: CrossAxisAlignment.start,
                    mainAxisSize: MainAxisSize.min,
                    children: <Widget>[
                      Row(
                        mainAxisAlignment: MainAxisAlignment.center,
                        children: <Widget>[
                          Text(
                            "\$ ${data.price}",
                            style: TextStyle(
                                color: Color.fromARGB(255, 26, 60, 117),
                                fontWeight: FontWeight.bold,
                                fontSize: 22.0),
                          ),
                          SizedBox(
                            width: 5,
                          ),
                          Text(
                            "/per night",
                            style:
                                TextStyle(fontSize: 14.0, color: Colors.grey),
                          )
                        ],
                      ),
                      const SizedBox(height: 20.0),
                      Container(
                        height: 50,
                        width: 400,
                        decoration: BoxDecoration(
                            borderRadius: BorderRadius.circular(50),
                            gradient: const LinearGradient(colors: [
                              Color.fromARGB(255, 0, 69, 189),
                              Color.fromARGB(255, 10, 158, 227)
                            ])),
                        child: InkWell(
                          child: const Center(child: Text("Book Now!")),
                        ),
                      ),
                      const SizedBox(height: 30.0),
                      Text(
                        "Description".toUpperCase(),
                        style: TextStyle(
                            fontWeight: FontWeight.w600, fontSize: 14.0),
                      ),
                      const SizedBox(height: 10.0),
                      Text(
                        data.roomType!.description!,
                        textAlign: TextAlign.justify,
                        style: TextStyle(
                            fontWeight: FontWeight.w300, fontSize: 14.0),
                      ),
                      const SizedBox(height: 30.0),
                      Text(
                        "Rules".toUpperCase(),
                        style: TextStyle(
                            fontWeight: FontWeight.w600, fontSize: 14.0),
                      ),
                      const SizedBox(height: 10.0),
                      Text(
                        data.roomType!.rules!,
                        textAlign: TextAlign.justify,
                        style: TextStyle(
                            fontWeight: FontWeight.w300, fontSize: 14.0),
                      ),
                      const SizedBox(height: 30.0),
                      Text(
                        "Amenities".toUpperCase(),
                        style: TextStyle(
                            fontWeight: FontWeight.w600, fontSize: 14.0),
                      ),
                      const SizedBox(height: 10.0),
                      Container(
                        height: 75,
                        child: GridView(
                          gridDelegate:
                              SliverGridDelegateWithFixedCrossAxisCount(
                                  crossAxisCount: 1,
                                  childAspectRatio: 1,
                                  crossAxisSpacing: 20,
                                  mainAxisSpacing: 30),
                          scrollDirection: Axis.horizontal,
                          children: _buildAmenitesGrid(),
                        ),
                      ),
                    ],
                  ),
                ),
              ],
            ),
          ),
        ],
      ),
    );
  }

  List<Widget> _buildAmenitesGrid() {
    if (data.amenities!.isEmpty) {
      return [const Text("No amenities...")];
    }

    List<Widget> list = data.roomAmenities!
        .map(
          (e) => Container(
            child: ClipOval(
              child: Material(
                child: Container(
                  child: Row(
                    mainAxisAlignment: MainAxisAlignment.spaceEvenly,
                    children: <Widget>[
                      Icon(
                        Icons.add_reaction_outlined,
                        color: Color.fromARGB(255, 10, 158, 227),
                      ),
                      Text(e.amenityName!),
                    ],
                  ),
                ),
              ),
            ),
          ),
        )
        .cast<Widget>()
        .toList();

    return list;
  }

  isSaved(RoomResponse x) {
    if (!x.isSaved!) {
      return GestureDetector(
        child: Padding(
          padding: const EdgeInsets.all(8.0),
          child: Icon(Icons.favorite_border_outlined,
              size: 34, color: Colors.white),
        ),
        onTap: () async {
          String response = await _roomProvider!
              .save(AuthHelper.user!.userId!, x.roomId!.toInt());

          if (response.isNotEmpty) await loadData();
        },
      );
    } else {
      return GestureDetector(
        child: Padding(
          padding: const EdgeInsets.all(8.0),
          child: Icon(Icons.favorite, size: 34, color: Colors.white),
        ),
        onTap: () async {
          var response = await _roomProvider!
              .removeSaved(AuthHelper.user!.userId!, x.roomId!.toInt());

          if (response.isNotEmpty) await loadData();
        },
      );
    }
  }
}
